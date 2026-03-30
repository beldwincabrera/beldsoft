using Beldsoft.Domain.Entities;

namespace Beldsoft.Application.DTOs;

public class TeamMemberDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public SocialLinks Socials { get; set; } = new();
    public List<SkillItem> Skills { get; set; } = [];
    public List<ExperienceItem> Experience { get; set; } = [];
}
