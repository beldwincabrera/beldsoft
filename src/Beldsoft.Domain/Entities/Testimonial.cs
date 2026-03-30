namespace Beldsoft.Domain.Entities;

public class Testimonial
{
    public int Id { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientTitle { get; set; } = string.Empty;
    public string ClientCompany { get; set; } = string.Empty;
    public string ClientImage { get; set; } = string.Empty;
    public string Quote { get; set; } = string.Empty;
    public int Rating { get; set; } = 5;
}
