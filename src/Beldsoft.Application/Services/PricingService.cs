using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class PricingService : IPricingService
{
    private static readonly List<PricingPlanDto> _plans =
    [
        new PricingPlanDto { Id = 1, Name = "BASIC", Subtitle = "For Startups", Description = "Everything you need to get started.", MonthlyPrice = 29, YearlyPrice = 290, IsHighlighted = false, IconPath = "assets/images/icons/icon-1.png", Features = ["5 Projects", "10 GB Storage", "Email Support", "Basic Analytics", "1 Team Member"] },
        new PricingPlanDto { Id = 2, Name = "Professional", Subtitle = "Most Popular", Description = "Advanced features for growing teams.", MonthlyPrice = 79, YearlyPrice = 790, IsHighlighted = true, IconPath = "assets/images/icons/icon-2.png", Features = ["25 Projects", "100 GB Storage", "Priority Support", "Advanced Analytics", "10 Team Members", "API Access", "Custom Integrations"] },
        new PricingPlanDto { Id = 3, Name = "Enterprise", Subtitle = "For Large Teams", Description = "Unlimited power for enterprise scale.", MonthlyPrice = 199, YearlyPrice = 1990, IsHighlighted = false, IconPath = "assets/images/icons/icon-3.png", Features = ["Unlimited Projects", "1 TB Storage", "24/7 Dedicated Support", "Full Analytics Suite", "Unlimited Members", "White Label", "SLA Guarantee", "On-Premise Option"] },
    ];

    public Task<IEnumerable<PricingPlanDto>> GetPlansAsync() =>
        Task.FromResult<IEnumerable<PricingPlanDto>>(_plans);
}
