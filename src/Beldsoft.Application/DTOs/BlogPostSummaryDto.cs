namespace Beldsoft.Application.DTOs;

public class BlogPostSummaryDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string FeaturedImage { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public int CommentCount { get; set; }
    public List<string> Categories { get; set; } = [];
    public List<string> Tags { get; set; } = [];
}
