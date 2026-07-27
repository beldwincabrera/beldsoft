using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class ServiceService : IServiceService
{
    private static readonly IReadOnlyList<ServiceDto> Services =
    [
        new() {
            Id = 1, Slug = "custom-software-development", Title = "Custom Software Development",
            ShortDescription = "Purpose-built web and business applications that replace manual work and fit the way your organization operates.",
            Description = "We design and build maintainable software around your workflows, users, and business goals. Engagements can cover discovery, architecture, implementation, testing, deployment, and handoff.",
            Icon = "code", Features = ["Business and web applications", "Responsive user experiences", "API and data integration", "Testing and deployment readiness"]
        },
        new() {
            Id = 2, Slug = "software-modernization", Title = "Software Modernization",
            ShortDescription = "Reduce the cost and risk of aging applications without disrupting the business they support.",
            Description = "We assess legacy systems, identify high-value improvements, and modernize in practical stages. The goal is a safer, faster, and more supportable platform—not change for its own sake.",
            Icon = "refresh", Features = ["Application and architecture assessment", "Incremental modernization roadmap", "Performance and reliability improvements", "Cloud-readiness planning"]
        },
        new() {
            Id = 3, Slug = "architecture-technical-leadership", Title = "Architecture & Technical Leadership",
            ShortDescription = "Senior technical direction for important decisions, complex initiatives, and growing engineering teams.",
            Description = "Beldsoft helps translate business priorities into sound technical plans. We clarify tradeoffs, reduce delivery risk, and establish an architecture your team can operate and evolve.",
            Icon = "architecture", Features = ["Solution and integration architecture", "Technical discovery and roadmaps", "Delivery-risk assessment", "Fractional technology leadership"]
        },
        new() {
            Id = 4, Slug = "apis-systems-integration", Title = "APIs & Systems Integration",
            ShortDescription = "Connect disconnected tools and data so information moves securely and work stops falling between systems.",
            Description = "We design APIs and integrations that make critical systems work together. Solutions emphasize clear contracts, secure access, observability, and resilient failure handling.",
            Icon = "link", Features = ["REST API design and development", "Third-party platform integration", "Identity and access integration", "Reliable messaging and workflow orchestration"]
        },
        new() {
            Id = 5, Slug = "workflow-automation-ai", Title = "Workflow Automation & Applied AI",
            ShortDescription = "Use automation and AI where they can remove repetitive effort, shorten cycle time, or improve decisions.",
            Description = "We identify practical opportunities, validate the business case, and build human-centered automation with appropriate controls. AI is treated as a capability—not a promise.",
            Icon = "automation", Features = ["Process discovery and automation", "AI-assisted internal workflows", "Human review and guardrails", "Integration with existing business systems"]
        },
        new() {
            Id = 6, Slug = "cloud-devops-reliability", Title = "Cloud, DevOps & Reliability",
            ShortDescription = "Make software easier to release, observe, scale, and support with disciplined cloud and delivery practices.",
            Description = "We improve deployment pipelines, runtime visibility, operational resilience, and cloud architecture so teams can ship changes with greater confidence.",
            Icon = "cloud", Features = ["Cloud architecture and migration planning", "CI/CD and release automation", "Monitoring and operational visibility", "Performance and resilience engineering"]
        }
    ];

    public Task<IEnumerable<ServiceDto>> GetServicesAsync() =>
        Task.FromResult<IEnumerable<ServiceDto>>(Services);

    public Task<ServiceDto?> GetServiceBySlugAsync(string slug) =>
        Task.FromResult(Services.FirstOrDefault(service =>
            string.Equals(service.Slug, slug, StringComparison.OrdinalIgnoreCase)));
}
