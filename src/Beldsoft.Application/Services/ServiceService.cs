using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class ServiceService : IServiceService
{
    private static readonly List<ServiceDto> _services =
    [
        new ServiceDto { Id = 1, Slug = "web-development", Title = "Custom Software Engineering", ShortDescription = "Secure web platforms, APIs, and business applications designed around your workflows and goals.", Icon = "fa-solid fa-code", Image = "assets/images/resource/services.webp", Features = ["Web Platforms", "APIs & Integrations", "SaaS Products", "Quality Engineering"] },
        new ServiceDto { Id = 2, Slug = "mobile-apps", Title = "Product Design & Discovery", ShortDescription = "Turn an opportunity into a validated product direction, usable experience, and achievable roadmap.", Icon = "fa-solid fa-pen-ruler", Image = "assets/images/resource/offer-1.webp", Features = ["Product Strategy", "UX Research", "Rapid Prototyping", "Design Systems"] },
        new ServiceDto { Id = 3, Slug = "cloud-solutions", Title = "Cloud & DevOps", ShortDescription = "Cloud-native architecture and delivery automation that improve resilience, speed, and operating cost.", Icon = "fa-solid fa-cloud", Image = "assets/images/resource/offer-2.webp", Features = ["Azure & AWS", "CI/CD", "Infrastructure as Code", "Observability"] },
        new ServiceDto { Id = 4, Slug = "cybersecurity", Title = "Application Modernization", ShortDescription = "Reduce legacy risk and evolve critical systems without disrupting the business that depends on them.", Icon = "fa-solid fa-arrows-rotate", Image = "assets/images/resource/offer-3.webp", Features = ["Architecture Assessment", "Incremental Migration", "Performance", "Security"] },
        new ServiceDto { Id = 5, Slug = "ai-solutions", Title = "AI & Intelligent Automation", ShortDescription = "Apply AI where it creates measurable value, with responsible architecture and production operations.", Icon = "fa-solid fa-brain", Image = "assets/images/resource/services.webp", Features = ["AI Product Discovery", "RAG & Assistants", "Workflow Automation", "MLOps"] },
        new ServiceDto { Id = 6, Slug = "it-consulting", Title = "Engineering Advisory", ShortDescription = "Independent technical guidance for architecture, delivery performance, platforms, and engineering teams.", Icon = "fa-solid fa-compass-drafting", Image = "assets/images/resource/offer-1.webp", Features = ["Technology Strategy", "Architecture Reviews", "Delivery Assessment", "Team Enablement"] },
    ];

    public Task<IEnumerable<ServiceDto>> GetServicesAsync() => Task.FromResult<IEnumerable<ServiceDto>>(_services);
    public Task<ServiceDto?> GetServiceBySlugAsync(string slug) =>
        Task.FromResult(_services.FirstOrDefault(s => s.Slug == slug));
}
