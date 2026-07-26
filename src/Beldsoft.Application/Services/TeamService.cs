using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class TeamService : ITeamService
{
    public Task<IEnumerable<TeamMemberDto>> GetMembersAsync() =>
        Task.FromResult<IEnumerable<TeamMemberDto>>([]);
    public Task<TeamMemberDto?> GetMemberBySlugAsync(string slug) =>
        Task.FromResult<TeamMemberDto?>(null);
}
