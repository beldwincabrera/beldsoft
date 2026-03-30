namespace Beldsoft.Domain.Entities;

public class BlogComment
{
    public int Id { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PostedAt { get; set; }
}
