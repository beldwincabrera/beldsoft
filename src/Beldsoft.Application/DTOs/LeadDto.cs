using System.ComponentModel.DataAnnotations;

namespace Beldsoft.Application.DTOs;

public sealed class LeadDto : IValidatableObject
{
    private static readonly HashSet<string> ContactMethods =
        new(StringComparer.OrdinalIgnoreCase) { "Email", "Phone" };

    private static readonly HashSet<string> ServiceInterests =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Custom software development",
            "Software modernization",
            "Architecture and technical leadership",
            "APIs and systems integration",
            "Workflow automation and applied AI",
            "Cloud, DevOps and reliability",
            "Not sure yet"
        };

    private static readonly HashSet<string> ProjectTimelines =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "As soon as possible",
            "Within 1–3 months",
            "Within 3–6 months",
            "More than 6 months",
            "Exploring options"
        };

    [Display(Name = "Full name")]
    [Required(ErrorMessage = "Enter your full name.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Enter a name between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Business or organization")]
    [StringLength(150, ErrorMessage = "Keep your business or organization name under 150 characters.")]
    public string BusinessName { get; set; } = string.Empty;

    [Display(Name = "Role or job title")]
    [StringLength(100, ErrorMessage = "Keep your role or job title under 100 characters.")]
    public string Role { get; set; } = string.Empty;

    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(254, ErrorMessage = "Keep your email address under 254 characters.")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Phone number")]
    [StringLength(30, ErrorMessage = "Keep your phone number under 30 characters.")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Area of interest")]
    [StringLength(100)]
    public string ServiceInterest { get; set; } = string.Empty;

    [Display(Name = "Preferred contact method")]
    [StringLength(30)]
    public string PreferredContactMethod { get; set; } = string.Empty;

    [Display(Name = "Best time to contact you")]
    [StringLength(100, ErrorMessage = "Keep your preferred contact time under 100 characters.")]
    public string PreferredContactTime { get; set; } = string.Empty;

    [Display(Name = "Expected timeline")]
    [StringLength(100)]
    public string ProjectTimeline { get; set; } = string.Empty;

    [Display(Name = "Project or business challenge")]
    [Required(ErrorMessage = "Describe the project or business challenge.")]
    [StringLength(3000, MinimumLength = 20, ErrorMessage = "Provide between 20 and 3,000 characters.")]
    public string Message { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Please acknowledge the privacy notice.")]
    public bool Consent { get; set; }

    // Honeypot: legitimate visitors never see or complete this field.
    public string Website { get; set; } = string.Empty;

    [StringLength(500)]
    public string SourcePage { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var contactMethod = PreferredContactMethod?.Trim() ?? string.Empty;
        var serviceInterest = ServiceInterest?.Trim() ?? string.Empty;
        var projectTimeline = ProjectTimeline?.Trim() ?? string.Empty;
        var phone = Phone?.Trim() ?? string.Empty;
        var businessName = BusinessName?.Trim() ?? string.Empty;

        if (!string.IsNullOrEmpty(businessName) && businessName.Length < 2)
        {
            yield return new ValidationResult(
                "Enter at least 2 characters for your business or organization.",
                [nameof(BusinessName)]);
        }

        if (!string.IsNullOrEmpty(phone) && !new PhoneAttribute().IsValid(phone))
        {
            yield return new ValidationResult(
                "Enter a valid phone number.",
                [nameof(Phone)]);
        }

        if (!string.IsNullOrEmpty(contactMethod) && !ContactMethods.Contains(contactMethod))
        {
            yield return new ValidationResult(
                "Select email or phone as your preferred contact method.",
                [nameof(PreferredContactMethod)]);
        }

        if (!string.IsNullOrEmpty(serviceInterest) && !ServiceInterests.Contains(serviceInterest))
        {
            yield return new ValidationResult(
                "Select a valid area of interest.",
                [nameof(ServiceInterest)]);
        }

        if (!string.IsNullOrEmpty(projectTimeline) && !ProjectTimelines.Contains(projectTimeline))
        {
            yield return new ValidationResult(
                "Select a valid expected timeline.",
                [nameof(ProjectTimeline)]);
        }

        if (string.Equals(contactMethod, "Phone", StringComparison.OrdinalIgnoreCase) &&
            string.IsNullOrWhiteSpace(Phone))
        {
            yield return new ValidationResult(
                "A phone number is required when phone is your preferred contact method.",
                [nameof(Phone)]);
        }
    }
}
