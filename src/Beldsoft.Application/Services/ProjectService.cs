using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class ProjectService : IProjectService
{
    private static readonly List<ProjectDto> _projects =
    [
        new ProjectDto { Id = 1, Slug = "fintech-platform", Title = "FinTech Platform", Category = "Web Development", Description = "A comprehensive financial technology platform with real-time trading, portfolio management, and advanced analytics.", Client = "FinCorp Inc.", Image = "assets/images/gallery/1.webp", Technologies = [".NET 9", "React", "Azure", "SQL Server"] },
        new ProjectDto { Id = 2, Slug = "healthcare-app", Title = "Healthcare Mobile App", Category = "Mobile Apps", Description = "Patient management and telemedicine application with HIPAA compliance.", Client = "MedCare Group", Image = "assets/images/gallery/2.jpg", Technologies = ["Flutter", "Firebase", "Node.js"] },
        new ProjectDto { Id = 3, Slug = "ecommerce-solution", Title = "E-Commerce Solution", Category = "Web Development", Description = "High-performance e-commerce platform supporting 50k+ daily transactions.", Client = "ShopGlobal", Image = "assets/images/gallery/3.webp", Technologies = ["Blazor", "EF Core", "Azure"] },
        new ProjectDto { Id = 4, Slug = "ai-analytics-dashboard", Title = "AI Analytics Dashboard", Category = "AI Solutions", Description = "Business intelligence platform with machine learning-powered forecasting.", Client = "DataDriven Ltd.", Image = "assets/images/gallery/4.webp", Technologies = ["Python", "TensorFlow", "Power BI"] },
        new ProjectDto { Id = 5, Slug = "cloud-migration", Title = "Cloud Migration Project", Category = "Cloud Solutions", Description = "Enterprise legacy system migration to Azure with zero downtime.", Client = "RetailMax", Image = "assets/images/gallery/5.webp", Technologies = ["Azure", "Kubernetes", "Terraform"] },
        new ProjectDto { Id = 6, Slug = "security-audit", Title = "Security Audit & Hardening", Category = "Cybersecurity", Description = "Comprehensive security overhaul for a banking institution.", Client = "SecureBank", Image = "assets/images/gallery/6.jpg", Technologies = ["SIEM", "Zero Trust", "MFA"] },
    ];

    public Task<IEnumerable<ProjectDto>> GetProjectsAsync() => Task.FromResult<IEnumerable<ProjectDto>>(_projects);
    public Task<ProjectDto?> GetProjectBySlugAsync(string slug) =>
        Task.FromResult(_projects.FirstOrDefault(p => p.Slug == slug));
}
