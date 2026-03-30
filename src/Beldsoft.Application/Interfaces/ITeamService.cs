using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface ITeamService
{
    Task<IEnumerable<TeamMemberDto>> GetMembersAsync();
    Task<TeamMemberDto?> GetMemberBySlugAsync(string slug);
}
