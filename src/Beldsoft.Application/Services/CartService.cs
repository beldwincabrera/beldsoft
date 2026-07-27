using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class CartService : ICartService
{
    private readonly List<CartItemDto> _items = [];

    public CartDto Cart => new() { Items = [.. _items] };
    public int TotalCount => _items.Sum(i => i.Quantity);
    public event Action? OnCartChanged;

    public void AddItem(ProductDto product, int quantity = 1)
    {
        var existing = _items.FirstOrDefault(i => i.ProductId == product.Id);
        if (existing is not null)
            existing.Quantity += quantity;
        else
            _items.Add(new CartItemDto { ProductId = product.Id, ProductName = product.Name, ProductImage = product.Images.FirstOrDefault() ?? "", UnitPrice = product.DisplayPrice, Quantity = quantity });
        OnCartChanged?.Invoke();
    }

    public void RemoveItem(int productId)
    {
        _items.RemoveAll(i => i.ProductId == productId);
        OnCartChanged?.Invoke();
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null) return;
        if (quantity <= 0) RemoveItem(productId);
        else { item.Quantity = quantity; OnCartChanged?.Invoke(); }
    }

    public void Clear()
    {
        _items.Clear();
        OnCartChanged?.Invoke();
    }
}
