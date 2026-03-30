namespace Beldsoft.Application.DTOs;

public class BlogPostDto : BlogPostSummaryDto
{
    public string Content { get; set; } = string.Empty;
    public string AuthorBio { get; set; } = string.Empty;
    public List<BlogCommentDto> Comments { get; set; } = [];
}
