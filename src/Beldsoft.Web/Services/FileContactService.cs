using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace Beldsoft.Web.Services;

public sealed class FileContactService(
    IOptions<ContactStorageOptions> options,
    IWebHostEnvironment environment,
    ILogger<FileContactService> logger) : IContactService
{
    private static readonly SemaphoreSlim FileLock = new(1, 1);
    private static readonly ConcurrentDictionary<string, List<DateTimeOffset>> Attempts = new();
    private static readonly ConcurrentDictionary<string, DateTimeOffset> RecentSubmissions = new();
    private readonly ContactStorageOptions _options = options.Value;

    public async Task<bool> SubmitMessageAsync(ContactFormDto form)
    {
        var validationContext = new ValidationContext(form);
        if (!Validator.TryValidateObject(form, validationContext, [], validateAllProperties: true))
        {
            logger.LogWarning("Contact submission rejected by server-side validation.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(form.Website))
        {
            logger.LogWarning("Contact submission rejected by spam protection.");
            return false;
        }

        var now = DateTimeOffset.UtcNow;
        var identity = Hash(form.Email.Trim().ToLowerInvariant());
        var attempts = Attempts.GetOrAdd(identity, _ => []);

        lock (attempts)
        {
            attempts.RemoveAll(timestamp => timestamp < now.AddHours(-1));
            if (attempts.Count >= _options.MaximumSubmissionsPerHour)
            {
                logger.LogWarning("Contact submission rejected by rate limit.");
                return false;
            }
            attempts.Add(now);
        }

        var fingerprint = Hash($"{identity}|{form.Message.Trim()}");
        if (RecentSubmissions.TryGetValue(fingerprint, out var submittedAt) &&
            submittedAt > now.AddMinutes(-_options.DuplicateWindowMinutes))
        {
            logger.LogWarning("Duplicate contact submission rejected.");
            return false;
        }

        var directory = Path.IsPathRooted(_options.Directory)
            ? _options.Directory
            : Path.Combine(environment.ContentRootPath, _options.Directory);
        var record = new ContactSubmissionRecord(
            Guid.NewGuid(), now, form.Name.Trim(), form.BusinessName.Trim(), form.Email.Trim(),
            form.Phone.Trim(), form.ServiceInterest.Trim(), form.PreferredContactMethod.Trim(),
            form.Message.Trim(), form.SourcePage.Trim());

        try
        {
            Directory.CreateDirectory(directory);
            var path = Path.Combine(directory, $"{now:yyyy-MM}.jsonl");
            var json = JsonSerializer.Serialize(record);

            await FileLock.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(path, json + Environment.NewLine, Encoding.UTF8);
            }
            finally
            {
                FileLock.Release();
            }

            RecentSubmissions[fingerprint] = now;
            logger.LogInformation("Contact submission {SubmissionId} accepted.", record.Id);
            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Contact submission could not be stored.");
            return false;
        }
    }

    private static string Hash(string value) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));

    private sealed record ContactSubmissionRecord(
        Guid Id,
        DateTimeOffset SubmittedAtUtc,
        string Name,
        string BusinessName,
        string Email,
        string Phone,
        string ServiceInterest,
        string PreferredContactMethod,
        string Message,
        string SourcePage);
}
