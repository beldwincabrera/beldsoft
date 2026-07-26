using System.ComponentModel.DataAnnotations;

namespace Beldsoft.Application.DTOs;

public sealed class ContactFormDto
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(150, MinimumLength = 2)]
    public string BusinessName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string ServiceInterest { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string PreferredContactMethod { get; set; } = string.Empty;

    [Required, StringLength(3000, MinimumLength = 20)]
    public string Message { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Please acknowledge the privacy notice.")]
    public bool Consent { get; set; }

    // Honeypot: legitimate visitors never see or complete this field.
    [StringLength(0)]
    public string Website { get; set; } = string.Empty;

    [StringLength(500)]
    public string SourcePage { get; set; } = string.Empty;
}
