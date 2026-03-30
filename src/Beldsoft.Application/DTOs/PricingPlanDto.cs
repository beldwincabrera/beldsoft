namespace Beldsoft.Application.DTOs;

public class PricingPlanDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MonthlyPrice { get; set; }
    public decimal YearlyPrice { get; set; }
    public List<string> Features { get; set; } = [];
    public bool IsHighlighted { get; set; }
    public string IconPath { get; set; } = string.Empty;
}
