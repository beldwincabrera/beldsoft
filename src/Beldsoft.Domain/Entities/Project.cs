namespace Beldsoft.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Client { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public List<string> Technologies { get; set; } = [];
    public DateTime CompletedAt { get; set; }
}
