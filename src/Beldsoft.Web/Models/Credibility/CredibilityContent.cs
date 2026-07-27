namespace Beldsoft.Web.Models.Credibility;

public sealed record ClientLogoItem(
    string Name,
    string LogoPath,
    string LogoAlternativeText,
    string? WebsiteUrl = null,
    bool IsApprovedForPublication = false)
{
    public bool IsReadyForPublication =>
        IsApprovedForPublication &&
        CredibilityContentGuard.IsResolved(Name) &&
        CredibilityContentGuard.IsResolved(LogoPath) &&
        CredibilityContentGuard.IsResolved(LogoAlternativeText) &&
        CredibilityContentGuard.IsResolvedWhenProvided(WebsiteUrl);
}

public sealed record ClientTestimonial(
    string Quote,
    string AttributionName,
    string AttributionTitle,
    string? Organization = null,
    bool IsApprovedForPublication = false)
{
    public bool IsReadyForPublication =>
        IsApprovedForPublication &&
        CredibilityContentGuard.IsResolved(Quote) &&
        CredibilityContentGuard.IsResolved(AttributionName) &&
        CredibilityContentGuard.IsResolved(AttributionTitle) &&
        CredibilityContentGuard.IsResolvedWhenProvided(Organization);
}

public sealed record LeadershipProfile(
    string PhotoPath,
    string PhotoAlternativeText,
    string Name,
    string Title,
    string Biography,
    bool IsApprovedForPublication = false)
{
    public bool IsReadyForPublication =>
        IsApprovedForPublication &&
        CredibilityContentGuard.IsResolved(PhotoPath) &&
        CredibilityContentGuard.IsResolved(PhotoAlternativeText) &&
        CredibilityContentGuard.IsResolved(Name) &&
        CredibilityContentGuard.IsResolved(Title) &&
        CredibilityContentGuard.IsResolved(Biography);
}

internal static class CredibilityContentGuard
{
    internal static bool IsResolved(string? value) =>
        !string.IsNullOrWhiteSpace(value) &&
        !value.Contains("TODO", StringComparison.OrdinalIgnoreCase);

    internal static bool IsResolvedWhenProvided(string? value) =>
        string.IsNullOrWhiteSpace(value) || IsResolved(value);
}
