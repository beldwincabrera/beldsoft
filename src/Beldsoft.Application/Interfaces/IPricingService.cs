using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface IPricingService
{
    Task<IEnumerable<PricingPlanDto>> GetPlansAsync();
}
