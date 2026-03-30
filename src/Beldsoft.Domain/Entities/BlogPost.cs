namespace Beldsoft.Domain.Entities;

public class BlogPost
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string FeaturedImage { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
    public string AuthorBio { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public int CommentCount { get; set; }
    public List<string> Categories { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public List<BlogComment> Comments { get; set; } = [];
}
