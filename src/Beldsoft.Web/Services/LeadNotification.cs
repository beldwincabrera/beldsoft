namespace Beldsoft.Web.Services;

internal sealed record LeadNotification(
    Guid LeadId,
    DateTimeOffset CreatedAtUtc,
    string RecipientAddress,
    string ReplyToAddress,
    string Subject,
    string PlainTextBody);
