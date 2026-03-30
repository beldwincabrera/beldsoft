using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface IBlogService
{
    Task<IEnumerable<BlogPostSummaryDto>> GetPostsAsync(int page = 1, int pageSize = 9);
    Task<BlogPostDto?> GetPostBySlugAsync(string slug);
    Task<IEnumerable<BlogPostSummaryDto>> GetRecentPostsAsync(int count = 3);
    Task<IEnumerable<string>> GetCategoriesAsync();
    Task<int> GetTotalCountAsync();
}
