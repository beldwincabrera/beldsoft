namespace Beldsoft.Application.DTOs;

public class ServiceDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<string> Features { get; set; } = [];
}
