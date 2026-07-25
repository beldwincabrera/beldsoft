using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;
using Beldsoft.Domain.Entities;

namespace Beldsoft.Application.Services;

public class TeamService : ITeamService
{
    private static readonly List<TeamMemberDto> _members =
    [
        new TeamMemberDto { Id = 1, Slug = "john-smith", Name = "John Smith", Title = "CEO & Founder", Photo = "assets/images/resource/team-1.webp", Bio = "Visionary leader with 15+ years in technology and enterprise solutions.", Email = "john@beldsoft.com", Phone = "+1 (555) 100-2000", Socials = new SocialLinks { Facebook = "#", Instagram = "#", Twitter = "#", LinkedIn = "#" }, Skills = [new SkillItem { Name = "Leadership", Percentage = 95 }, new SkillItem { Name = "Strategy", Percentage = 90 }, new SkillItem { Name = "Technology", Percentage = 85 }] },
        new TeamMemberDto { Id = 2, Slug = "jane-doe", Name = "Jane Doe", Title = "CTO", Photo = "assets/images/resource/team-2.webp", Bio = "Technology architect driving innovation with cutting-edge solutions.", Email = "jane@beldsoft.com", Phone = "+1 (555) 100-2001", Socials = new SocialLinks { Facebook = "#", Twitter = "#", LinkedIn = "#" }, Skills = [new SkillItem { Name = "Architecture", Percentage = 92 }, new SkillItem { Name = "Development", Percentage = 88 }, new SkillItem { Name = "Cloud", Percentage = 90 }] },
        new TeamMemberDto { Id = 3, Slug = "mike-johnson", Name = "Mike Johnson", Title = "Lead Developer", Photo = "assets/images/resource/team-3.webp", Bio = "Full-stack expert specializing in .NET and React ecosystems.", Email = "mike@beldsoft.com", Phone = "+1 (555) 100-2002", Socials = new SocialLinks { GitHub = "#", LinkedIn = "#" }, Skills = [new SkillItem { Name = ".NET / C#", Percentage = 95 }, new SkillItem { Name = "React", Percentage = 85 }, new SkillItem { Name = "Azure", Percentage = 80 }] },
        new TeamMemberDto { Id = 4, Slug = "sarah-lee", Name = "Sarah Lee", Title = "UX Designer", Photo = "assets/images/resource/team-4.webp", Bio = "User experience advocate crafting intuitive and beautiful interfaces.", Email = "sarah@beldsoft.com", Phone = "+1 (555) 100-2003", Socials = new SocialLinks { Instagram = "#", Dribbble = "#", LinkedIn = "#" }, Skills = [new SkillItem { Name = "Figma", Percentage = 95 }, new SkillItem { Name = "UX Research", Percentage = 88 }, new SkillItem { Name = "Prototyping", Percentage = 90 }] },
    ];

    public Task<IEnumerable<TeamMemberDto>> GetMembersAsync() => Task.FromResult<IEnumerable<TeamMemberDto>>(_members);
    public Task<TeamMemberDto?> GetMemberBySlugAsync(string slug) =>
        Task.FromResult(_members.FirstOrDefault(m => m.Slug == slug));
}
