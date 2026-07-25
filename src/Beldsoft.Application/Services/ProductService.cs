using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class ProductService : IProductService
{
    private static readonly List<ProductDto> _products =
    [
        new ProductDto { Id = 1, Slug = "it-security-guide", Name = "IT Security Guide", Description = "Comprehensive guide to enterprise cybersecurity best practices.", Price = 49.99m, Images = ["assets/images/resource/products/1.webp"], Category = "Books", StockQuantity = 100, Rating = 4.5, ReviewCount = 28 },
        new ProductDto { Id = 2, Slug = "cloud-strategy-toolkit", Name = "Cloud Strategy Toolkit", Description = "Templates and tools for planning your cloud migration.", Price = 79.99m, SalePrice = 59.99m, Images = ["assets/images/resource/products/2.webp"], Category = "Templates", StockQuantity = 50, Rating = 4.8, ReviewCount = 42 },
        new ProductDto { Id = 3, Slug = "devops-course", Name = "DevOps Mastery Course", Description = "Complete video course on modern DevOps practices.", Price = 129.99m, Images = ["assets/images/resource/products/3.webp"], Category = "Courses", StockQuantity = 999, Rating = 4.9, ReviewCount = 156 },
        new ProductDto { Id = 4, Slug = "ui-component-library", Name = "UI Component Library", Description = "Premium Blazor UI components for enterprise applications.", Price = 199.99m, Images = ["assets/images/resource/products/4.webp"], Category = "Software", StockQuantity = 999, Rating = 4.7, ReviewCount = 63 },
        new ProductDto { Id = 5, Slug = "api-design-handbook", Name = "API Design Handbook", Description = "Best practices for RESTful and GraphQL API design.", Price = 39.99m, Images = ["assets/images/resource/products/5.webp"], Category = "Books", StockQuantity = 200, Rating = 4.6, ReviewCount = 35 },
        new ProductDto { Id = 6, Slug = "monitoring-dashboard", Name = "Monitoring Dashboard Template", Description = "Ready-to-use monitoring and alerting dashboard.", Price = 89.99m, SalePrice = 69.99m, Images = ["assets/images/resource/products/6.webp"], Category = "Templates", StockQuantity = 150, Rating = 4.4, ReviewCount = 21 },
    ];

    public Task<IEnumerable<ProductDto>> GetProductsAsync(int page = 1, int pageSize = 9)
    {
        var result = _products.Skip((page - 1) * pageSize).Take(pageSize);
        return Task.FromResult(result);
    }

    public Task<ProductDto?> GetProductBySlugAsync(string slug) =>
        Task.FromResult(_products.FirstOrDefault(p => p.Slug == slug));

    public Task<IEnumerable<ProductDto>> GetRelatedProductsAsync(string category, int count = 4) =>
        Task.FromResult(_products.Where(p => p.Category == category).Take(count));

    public Task<int> GetTotalCountAsync() => Task.FromResult(_products.Count);
}
