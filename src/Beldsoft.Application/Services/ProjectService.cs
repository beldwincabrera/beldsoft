using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class ProjectService : IProjectService
{
    public Task<IEnumerable<ProjectDto>> GetProjectsAsync() =>
        Task.FromResult<IEnumerable<ProjectDto>>([]);
    public Task<ProjectDto?> GetProjectBySlugAsync(string slug) =>
        Task.FromResult<ProjectDto?>(null);
}
