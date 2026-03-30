using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetServicesAsync();
    Task<ServiceDto?> GetServiceBySlugAsync(string slug);
}
