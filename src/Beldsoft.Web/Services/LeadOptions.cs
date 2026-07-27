namespace Beldsoft.Web.Services;

public sealed class LeadOptions
{
    public const string SectionName = "Leads";

    public string StorageDirectory { get; set; } = "App_Data/leads";
    public string LocalEmailDropDirectory { get; set; } = "App_Data/lead-email-drop";
    public int DuplicateWindowMinutes { get; set; } = 10;
    public int MaximumSubmissionsPerHour { get; set; } = 5;
    public string RecipientAddress { get; set; } = "beldwin@beldsoft.com";
    public GraphEmailOptions Graph { get; set; } = new();

    public bool HasValidRecipientAddress => IsValidEmailAddress(RecipientAddress);

    private static bool IsValidEmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            var address = new System.Net.Mail.MailAddress(value);
            return string.Equals(
                address.Address,
                value.Trim(),
                StringComparison.OrdinalIgnoreCase);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}

public sealed class GraphEmailOptions
{
    public string TenantId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string SenderUserId { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;

    public bool IsConfigured =>
        HasValidTenantId &&
        Guid.TryParse(ClientId, out _) &&
        !string.IsNullOrWhiteSpace(ClientSecret) &&
        HasValidSenderUserId;

    private bool HasValidTenantId
    {
        get
        {
            var tenantId = TenantId?.Trim() ?? string.Empty;
            return Guid.TryParse(tenantId, out _) ||
                   (tenantId.Contains('.', StringComparison.Ordinal) &&
                    Uri.CheckHostName(tenantId) == UriHostNameType.Dns);
        }
    }

    private bool HasValidSenderUserId =>
        Guid.TryParse(SenderUserId, out _) || IsValidEmailAddress(SenderUserId);

    private static bool IsValidEmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            var address = new System.Net.Mail.MailAddress(value);
            return string.Equals(
                address.Address,
                value.Trim(),
                StringComparison.OrdinalIgnoreCase);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
