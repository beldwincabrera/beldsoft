using System.Globalization;
using System.Text;
using Microsoft.Extensions.Options;

namespace Beldsoft.Web.Services;

internal sealed class LocalEmailDropNotificationSender : ILeadNotificationSender
{
    private static readonly Encoding Utf8WithoutBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    private readonly ILogger<LocalEmailDropNotificationSender> _logger;

    public LocalEmailDropNotificationSender(
        IOptions<LeadOptions> options,
        IWebHostEnvironment environment,
        ILogger<LocalEmailDropNotificationSender> logger)
    {
        _logger = logger;
        DropDirectory = PrivateDataDirectoryResolver.Resolve(
            options.Value.LocalEmailDropDirectory,
            environment,
            "Leads:LocalEmailDropDirectory");
    }

    public string DeliveryMode => "local email drop";

    public string DropDirectory { get; }

    public async Task SendAsync(
        LeadNotification notification,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(DropDirectory);

        var fileName = $"{notification.CreatedAtUtc:yyyyMMddTHHmmssfffZ}-{notification.LeadId:N}.eml";
        var finalPath = Path.Combine(DropDirectory, fileName);
        var temporaryPath = $"{finalPath}.{Guid.NewGuid():N}.tmp";
        var content = BuildMimeMessage(notification);

        try
        {
            await using (var stream = new FileStream(
                temporaryPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                FileOptions.Asynchronous | FileOptions.WriteThrough))
            await using (var writer = new StreamWriter(stream, Utf8WithoutBom))
            {
                await writer.WriteAsync(content.AsMemory(), cancellationToken);
                await writer.FlushAsync(cancellationToken);
            }

            File.Move(temporaryPath, finalPath);
        }
        finally
        {
            if (File.Exists(temporaryPath))
            {
                File.Delete(temporaryPath);
            }
        }

        _logger.LogInformation(
            "Lead {LeadId} notification was written to the local email drop.",
            notification.LeadId);
    }

    private static string BuildMimeMessage(LeadNotification notification)
    {
        var normalizedBody = notification.PlainTextBody
            .Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n')
            .Replace("\n", "\r\n", StringComparison.Ordinal);
        var encodedBody = Convert.ToBase64String(
            Encoding.UTF8.GetBytes(normalizedBody),
            Base64FormattingOptions.InsertLineBreaks);

        var headers = new[]
        {
            $"Date: {notification.CreatedAtUtc.ToString("r", CultureInfo.InvariantCulture)}",
            $"Message-ID: <{notification.LeadId:N}@local.beldsoft.com>",
            "From: \"Beldsoft Website local notification\" <support@beldsoft.com>",
            $"To: <{SanitizeHeader(notification.RecipientAddress)}>",
            $"Reply-To: <{SanitizeHeader(notification.ReplyToAddress)}>",
            $"Subject: {SanitizeHeader(notification.Subject)}",
            "MIME-Version: 1.0",
            "Content-Type: text/plain; charset=utf-8",
            "Content-Transfer-Encoding: base64",
            string.Empty,
            encodedBody,
            string.Empty
        };

        return string.Join("\r\n", headers);
    }

    private static string SanitizeHeader(string value) =>
        value.Replace('\r', ' ').Replace('\n', ' ').Trim();
}
