namespace Beldsoft.Web.Services;

public sealed class ContactStorageOptions
{
    public const string SectionName = "ContactStorage";
    public string Directory { get; set; } = "App_Data/contact-submissions";
    public int DuplicateWindowMinutes { get; set; } = 10;
    public int MaximumSubmissionsPerHour { get; set; } = 5;
}
