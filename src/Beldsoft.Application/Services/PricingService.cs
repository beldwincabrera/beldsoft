using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class PricingService : IPricingService
{
    private static readonly List<PricingPlanDto> _plans =
    [
        new PricingPlanDto { Id = 1, Name = "Discovery Sprint", Subtitle = "Define the right investment", Description = "A focused engagement that turns an opportunity or technical challenge into a validated delivery plan.", IsHighlighted = false, IconPath = "assets/images/icons/price-1.svg", Features = ["Stakeholder workshops", "Technical assessment", "Experience direction", "Prioritized roadmap", "Delivery estimate"] },
        new PricingPlanDto { Id = 2, Name = "Project Delivery", Subtitle = "Build and launch", Description = "A cross-functional team accountable for taking a defined product or modernization initiative into production.", IsHighlighted = true, IconPath = "assets/images/icons/price-2.svg", Features = ["Dedicated delivery team", "Iterative releases", "Quality engineering", "Cloud and DevOps", "Knowledge transfer"] },
        new PricingPlanDto { Id = 3, Name = "Engineering Partnership", Subtitle = "Extend your capability", Description = "Ongoing engineering capacity for evolving products, reducing technical risk, and improving delivery performance.", IsHighlighted = false, IconPath = "assets/images/icons/price-3.svg", Features = ["Flexible team composition", "Roadmap delivery", "Architecture guidance", "Performance and reliability", "Operational support"] },
    ];

    public Task<IEnumerable<PricingPlanDto>> GetPlansAsync() =>
        Task.FromResult<IEnumerable<PricingPlanDto>>(_plans);
}
