namespace Beldsoft.Application.DTOs;

public class CartDto
{
    public List<CartItemDto> Items { get; set; } = [];
    public decimal Subtotal => Items.Sum(i => i.LineTotal);
    public decimal Shipping { get; set; } = 0;
    public decimal Total => Subtotal + Shipping;
    public int TotalCount => Items.Sum(i => i.Quantity);
}

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
}
