using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class BlogService : IBlogService
{
    private static readonly List<BlogPostSummaryDto> _posts =
    [
        new BlogPostSummaryDto
        {
            Id = 1, Slug = "the-future-of-ai-in-business", Title = "The Future of AI in Business",
            Excerpt = "Artificial intelligence is reshaping how businesses operate, from automation to predictive analytics.",
            FeaturedImage = "assets/images/resource/news-1.webp", Author = "John Smith", AuthorImage = "assets/images/resource/author-1.jpg",
            PublishedAt = new DateTime(2025, 3, 10), CommentCount = 4,
            Categories = ["Technology", "AI"], Tags = ["AI", "Business", "Innovation"]
        },
        new BlogPostSummaryDto
        {
            Id = 2, Slug = "cloud-migration-best-practices", Title = "Cloud Migration Best Practices",
            Excerpt = "Moving your infrastructure to the cloud requires careful planning and execution.",
            FeaturedImage = "assets/images/resource/news-2.webp", Author = "Jane Doe", AuthorImage = "assets/images/resource/author-2.jpg",
            PublishedAt = new DateTime(2025, 3, 5), CommentCount = 7,
            Categories = ["Cloud", "Infrastructure"], Tags = ["Cloud", "DevOps", "Migration"]
        },
        new BlogPostSummaryDto
        {
            Id = 3, Slug = "cybersecurity-trends-2025", Title = "Cybersecurity Trends for 2025",
            Excerpt = "Stay ahead of threats with an overview of the most important cybersecurity trends this year.",
            FeaturedImage = "assets/images/resource/news-3.webp", Author = "Mike Johnson", AuthorImage = "assets/images/resource/author-3.jpg",
            PublishedAt = new DateTime(2025, 2, 28), CommentCount = 12,
            Categories = ["Security"], Tags = ["Security", "Cybersecurity", "Trends"]
        },
        new BlogPostSummaryDto
        {
            Id = 4, Slug = "building-scalable-microservices", Title = "Building Scalable Microservices",
            Excerpt = "Microservices architecture enables teams to build and deploy independently.",
            FeaturedImage = "assets/images/resource/news-1.webp", Author = "Sarah Lee", AuthorImage = "assets/images/resource/author-1.jpg",
            PublishedAt = new DateTime(2025, 2, 20), CommentCount = 3,
            Categories = ["Architecture", "Development"], Tags = ["Microservices", ".NET", "Architecture"]
        },
        new BlogPostSummaryDto
        {
            Id = 5, Slug = "ux-design-principles", Title = "UX Design Principles for Tech Products",
            Excerpt = "Great user experience drives adoption and reduces churn in SaaS products.",
            FeaturedImage = "assets/images/resource/news-2.webp", Author = "Tom Brown", AuthorImage = "assets/images/resource/author-2.jpg",
            PublishedAt = new DateTime(2025, 2, 14), CommentCount = 8,
            Categories = ["Design", "UX"], Tags = ["UX", "Design", "Product"]
        },
        new BlogPostSummaryDto
        {
            Id = 6, Slug = "devops-pipeline-automation", Title = "DevOps Pipeline Automation",
            Excerpt = "Automated pipelines reduce manual errors and speed up delivery cycles.",
            FeaturedImage = "assets/images/resource/news-3.webp", Author = "John Smith", AuthorImage = "assets/images/resource/author-1.jpg",
            PublishedAt = new DateTime(2025, 2, 7), CommentCount = 5,
            Categories = ["DevOps", "Automation"], Tags = ["CI/CD", "DevOps", "Automation"]
        },
    ];

    public Task<IEnumerable<BlogPostSummaryDto>> GetPostsAsync(int page = 1, int pageSize = 9)
    {
        var result = _posts.Skip((page - 1) * pageSize).Take(pageSize);
        return Task.FromResult(result);
    }

    public Task<BlogPostDto?> GetPostBySlugAsync(string slug)
    {
        var summary = _posts.FirstOrDefault(p => p.Slug == slug);
        if (summary is null) return Task.FromResult<BlogPostDto?>(null);
        var post = new BlogPostDto
        {
            Id = summary.Id, Slug = summary.Slug, Title = summary.Title,
            Excerpt = summary.Excerpt, FeaturedImage = summary.FeaturedImage,
            Author = summary.Author, AuthorImage = summary.AuthorImage,
            PublishedAt = summary.PublishedAt, CommentCount = summary.CommentCount,
            Categories = summary.Categories, Tags = summary.Tags,
            Content = "<p>This is the full content of the blog post. It covers the topic in depth with multiple sections, examples, and insights for modern IT professionals.</p><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>",
            AuthorBio = "A seasoned IT professional with over 10 years of industry experience.",
            Comments = [new BlogCommentDto { Id = 1, AuthorName = "Alex W.", AuthorImage = "assets/images/resource/author-2.jpg", Content = "Great insights! Very helpful.", PostedAt = DateTime.UtcNow.AddDays(-1) }]
        };
        return Task.FromResult<BlogPostDto?>(post);
    }

    public Task<IEnumerable<BlogPostSummaryDto>> GetRecentPostsAsync(int count = 3) =>
        Task.FromResult(_posts.Take(count));

    public Task<IEnumerable<string>> GetCategoriesAsync() =>
        Task.FromResult(_posts.SelectMany(p => p.Categories).Distinct());

    public Task<int> GetTotalCountAsync() => Task.FromResult(_posts.Count);
}
