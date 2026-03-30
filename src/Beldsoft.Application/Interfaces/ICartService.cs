using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface ICartService
{
    CartDto Cart { get; }
    int TotalCount { get; }
    void AddItem(ProductDto product, int quantity = 1);
    void RemoveItem(int productId);
    void UpdateQuantity(int productId, int quantity);
    void Clear();
    event Action? OnCartChanged;
}
