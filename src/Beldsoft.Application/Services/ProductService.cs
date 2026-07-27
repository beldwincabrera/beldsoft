using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class ProductService : IProductService
{
    public Task<IEnumerable<ProductDto>> GetProductsAsync(int page = 1, int pageSize = 9) => Task.FromResult<IEnumerable<ProductDto>>([]);
    public Task<ProductDto?> GetProductBySlugAsync(string slug) => Task.FromResult<ProductDto?>(null);
    public Task<IEnumerable<ProductDto>> GetRelatedProductsAsync(string category, int count = 4) => Task.FromResult<IEnumerable<ProductDto>>([]);
    public Task<int> GetTotalCountAsync() => Task.FromResult(0);
}
