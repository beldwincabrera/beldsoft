namespace Beldsoft.Web.Services;

internal static class PrivateDataDirectoryResolver
{
    public static string Resolve(
        string configuredDirectory,
        IWebHostEnvironment environment,
        string settingName)
    {
        if (string.IsNullOrWhiteSpace(configuredDirectory))
        {
            throw new InvalidOperationException(
                $"{settingName} must identify a private data folder.");
        }

        var resolvedDirectory = Path.GetFullPath(
            Path.IsPathRooted(configuredDirectory)
                ? configuredDirectory
                : Path.Combine(environment.ContentRootPath, configuredDirectory));
        var pathRoot = Path.GetPathRoot(resolvedDirectory);
        var comparison = OperatingSystem.IsWindows()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;

        if (!string.IsNullOrWhiteSpace(pathRoot) &&
            string.Equals(
                Path.TrimEndingDirectorySeparator(resolvedDirectory),
                Path.TrimEndingDirectorySeparator(pathRoot),
                comparison))
        {
            throw new InvalidOperationException(
                $"{settingName} cannot be a filesystem root.");
        }

        var webRoot = Path.GetFullPath(
            string.IsNullOrWhiteSpace(environment.WebRootPath)
                ? Path.Combine(environment.ContentRootPath, "wwwroot")
                : environment.WebRootPath);

        if (IsSameOrChildPath(resolvedDirectory, webRoot, comparison))
        {
            throw new InvalidOperationException(
                $"{settingName} cannot be inside the public web root.");
        }

        return resolvedDirectory;
    }

    private static bool IsSameOrChildPath(
        string candidate,
        string parent,
        StringComparison comparison)
    {
        var normalizedCandidate = Path.TrimEndingDirectorySeparator(candidate);
        var normalizedParent = Path.TrimEndingDirectorySeparator(parent);

        return string.Equals(normalizedCandidate, normalizedParent, comparison) ||
               normalizedCandidate.StartsWith(
                   $"{normalizedParent}{Path.DirectorySeparatorChar}",
                   comparison);
    }
}
