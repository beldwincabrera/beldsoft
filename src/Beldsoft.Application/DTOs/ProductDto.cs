namespace Beldsoft.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal DisplayPrice => SalePrice ?? Price;
    public List<string> Images { get; set; } = [];
    public string Category { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
}
