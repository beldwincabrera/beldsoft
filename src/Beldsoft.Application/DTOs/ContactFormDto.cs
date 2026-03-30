using System.ComponentModel.DataAnnotations;

namespace Beldsoft.Application.DTOs;

public class ContactFormDto
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    [Required] public string Message { get; set; } = string.Empty;
}
