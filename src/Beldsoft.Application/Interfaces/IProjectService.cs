using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetProjectsAsync();
    Task<ProjectDto?> GetProjectBySlugAsync(string slug);
}
