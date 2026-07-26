using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class PricingService : IPricingService
{
    public Task<IEnumerable<PricingPlanDto>> GetPlansAsync() =>
        Task.FromResult<IEnumerable<PricingPlanDto>>([]);
}
