using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace Beldsoft.Web.Services;

internal sealed class LeadService(
    IOptions<LeadOptions> options,
    IWebHostEnvironment environment,
    ILeadNotificationSender notificationSender,
    ILogger<LeadService> logger) : ILeadService
{
    private static readonly SemaphoreSlim FileLock = new(1, 1);
    private static readonly ConcurrentDictionary<string, List<DateTimeOffset>> Attempts = new();
    private static readonly ConcurrentDictionary<string, DateTimeOffset> RecentSubmissions = new();
    private readonly LeadOptions _options = options.Value;
    private readonly string _storageDirectory = PrivateDataDirectoryResolver.Resolve(
        options.Value.StorageDirectory,
        environment,
        "Leads:StorageDirectory");

    public async Task<bool> SubmitLeadAsync(LeadDto lead)
    {
        var validationContext = new ValidationContext(lead);
        if (!Validator.TryValidateObject(lead, validationContext, [], validateAllProperties: true))
        {
            logger.LogWarning("Lead rejected by server-side validation.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(lead.Website))
        {
            logger.LogWarning("Lead rejected by spam protection.");
            return false;
        }

        var now = DateTimeOffset.UtcNow;
        var identity = Hash(lead.Email.Trim().ToLowerInvariant());
        var attempts = Attempts.GetOrAdd(identity, _ => []);

        lock (attempts)
        {
            attempts.RemoveAll(timestamp => timestamp < now.AddHours(-1));
            if (attempts.Count >= _options.MaximumSubmissionsPerHour)
            {
                logger.LogWarning("Lead rejected by rate limit.");
                return false;
            }

            attempts.Add(now);
        }

        RemoveExpiredEntries(now);

        var fingerprint = Hash($"{identity}|{lead.Message?.Trim()}");
        if (!TryReserveSubmission(fingerprint, now))
        {
            logger.LogWarning("Duplicate lead rejected.");
            return false;
        }

        var record = new LeadRecord(
            Guid.NewGuid(),
            now,
            Normalize(lead.Name),
            Normalize(lead.BusinessName),
            Normalize(lead.Role),
            Normalize(lead.Email),
            Normalize(lead.Phone),
            Normalize(lead.ServiceInterest),
            Normalize(lead.PreferredContactMethod),
            Normalize(lead.PreferredContactTime),
            Normalize(lead.ProjectTimeline),
            Normalize(lead.Message),
            Normalize(lead.SourcePage),
            lead.Consent,
            "2026-07-26");

        string pendingPath;
        try
        {
            pendingPath = await StorePendingLeadAsync(record);
        }
        catch (Exception exception)
        {
            RecentSubmissions.TryRemove(fingerprint, out _);
            logger.LogError(exception, "Lead {LeadId} could not be placed in durable pending storage.", record.Id);
            return false;
        }

        try
        {
            var notification = new LeadNotification(
                record.Id,
                record.SubmittedAtUtc,
                _options.RecipientAddress,
                record.Email,
                $"New Beldsoft Lead - {record.Id:N}",
                BuildPlainTextBody(record));

            await notificationSender.SendAsync(notification);
            logger.LogInformation(
                "Lead {LeadId} notification was accepted by {DeliveryMode}.",
                record.Id,
                notificationSender.DeliveryMode);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Lead {LeadId} notification could not be handed off and remains in pending storage.",
                record.Id);
            return false;
        }

        try
        {
            await MarkLeadAcceptedAsync(record, pendingPath);
        }
        catch (Exception exception)
        {
            // Notification handoff is the acceptance boundary. Do not ask the prospect to
            // resubmit and create a duplicate when only the status transition failed.
            logger.LogError(
                exception,
                "Lead {LeadId} notification was accepted but its local status could not be updated.",
                record.Id);
        }

        return true;
    }

    private async Task<string> StorePendingLeadAsync(LeadRecord lead)
    {
        var pendingDirectory = Path.Combine(_storageDirectory, "pending");

        Directory.CreateDirectory(pendingDirectory);
        var path = Path.Combine(pendingDirectory, $"{lead.Id:N}.json");
        var temporaryPath = $"{path}.{Guid.NewGuid():N}.tmp";
        var json = JsonSerializer.SerializeToUtf8Bytes(lead);

        await FileLock.WaitAsync();
        try
        {
            try
            {
                await using (var stream = new FileStream(
                    temporaryPath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    FileOptions.Asynchronous | FileOptions.WriteThrough))
                {
                    await stream.WriteAsync(json);
                    await stream.FlushAsync();
                }

                File.Move(temporaryPath, path);
            }
            finally
            {
                if (File.Exists(temporaryPath))
                {
                    File.Delete(temporaryPath);
                }
            }
        }
        finally
        {
            FileLock.Release();
        }

        return path;
    }

    private async Task MarkLeadAcceptedAsync(LeadRecord lead, string pendingPath)
    {
        var acceptedDirectory = Path.Combine(_storageDirectory, "accepted", $"{lead.SubmittedAtUtc:yyyy-MM}");
        var acceptedPath = Path.Combine(acceptedDirectory, $"{lead.Id:N}.json");

        Directory.CreateDirectory(acceptedDirectory);
        await FileLock.WaitAsync();
        try
        {
            File.Move(pendingPath, acceptedPath, overwrite: true);
        }
        finally
        {
            FileLock.Release();
        }
    }

    private static string BuildPlainTextBody(LeadRecord lead)
    {
        var builder = new StringBuilder();
        builder.AppendLine("A new Lead was submitted through beldsoft.com.");
        builder.AppendLine();
        Append(builder, "Lead ID", lead.Id.ToString());
        Append(builder, "Submitted (UTC)", lead.SubmittedAtUtc.ToString("u"));
        Append(builder, "Full name", lead.Name);
        Append(builder, "Business / organization", lead.BusinessName);
        Append(builder, "Role / title", lead.Role);
        Append(builder, "Email", lead.Email);
        Append(builder, "Phone", lead.Phone);
        Append(builder, "Preferred contact method", lead.PreferredContactMethod);
        Append(builder, "Best contact time", lead.PreferredContactTime);
        Append(builder, "Area of interest", lead.ServiceInterest);
        Append(builder, "Project timeline", lead.ProjectTimeline);
        Append(builder, "Source page", lead.SourcePage);
        Append(builder, "Privacy acknowledgement", lead.ConsentAccepted ? $"Accepted ({lead.PrivacyNoticeVersion})" : "Not accepted");
        builder.AppendLine();
        builder.AppendLine("Business need / project description");
        builder.AppendLine("-----------------------------------");
        builder.AppendLine(lead.Message);
        builder.AppendLine();
        builder.AppendLine("Reply to this email to contact the prospect directly.");
        return builder.ToString();
    }

    private static void Append(StringBuilder builder, string label, string value) =>
        builder.AppendLine($"{label}: {(string.IsNullOrWhiteSpace(value) ? "Not provided" : value)}");

    private bool TryReserveSubmission(string fingerprint, DateTimeOffset now)
    {
        while (true)
        {
            if (!RecentSubmissions.TryGetValue(fingerprint, out var existing))
            {
                if (RecentSubmissions.TryAdd(fingerprint, now))
                {
                    return true;
                }

                continue;
            }

            if (existing > now.AddMinutes(-_options.DuplicateWindowMinutes))
            {
                return false;
            }

            if (RecentSubmissions.TryUpdate(fingerprint, now, existing))
            {
                return true;
            }
        }
    }

    private void RemoveExpiredEntries(DateTimeOffset now)
    {
        foreach (var entry in Attempts)
        {
            lock (entry.Value)
            {
                entry.Value.RemoveAll(timestamp => timestamp < now.AddHours(-1));
                if (entry.Value.Count == 0)
                {
                    Attempts.TryRemove(entry.Key, out _);
                }
            }
        }

        foreach (var entry in RecentSubmissions)
        {
            if (entry.Value < now.AddMinutes(-Math.Max(1, _options.DuplicateWindowMinutes)))
            {
                RecentSubmissions.TryRemove(entry.Key, out _);
            }
        }
    }

    private static string Normalize(string? value) => value?.Trim() ?? string.Empty;

    private static string Hash(string value) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));

    private sealed record LeadRecord(
        Guid Id,
        DateTimeOffset SubmittedAtUtc,
        string Name,
        string BusinessName,
        string Role,
        string Email,
        string Phone,
        string ServiceInterest,
        string PreferredContactMethod,
        string PreferredContactTime,
        string ProjectTimeline,
        string Message,
        string SourcePage,
        bool ConsentAccepted,
        string PrivacyNoticeVersion);
}
