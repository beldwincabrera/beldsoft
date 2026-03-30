namespace Beldsoft.Domain.Entities;

public class CartItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal LineTotal => (Product.SalePrice ?? Product.Price) * Quantity;
}
