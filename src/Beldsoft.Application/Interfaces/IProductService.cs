using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(int page = 1, int pageSize = 9);
    Task<ProductDto?> GetProductBySlugAsync(string slug);
    Task<IEnumerable<ProductDto>> GetRelatedProductsAsync(string category, int count = 4);
    Task<int> GetTotalCountAsync();
}
