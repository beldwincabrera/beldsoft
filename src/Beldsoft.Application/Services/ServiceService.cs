using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class ServiceService : IServiceService
{
    private static readonly List<ServiceDto> _services =
    [
        new ServiceDto { Id = 1, Slug = "web-development", Title = "Web Development", ShortDescription = "Custom web applications built with modern technologies.", Icon = "fa-solid fa-globe", Image = "assets/images/resource/service-1.jpg", Features = ["Responsive Design", "Performance Optimized", "SEO Ready", "Cross-Browser Compatible"] },
        new ServiceDto { Id = 2, Slug = "mobile-apps", Title = "Mobile Applications", ShortDescription = "Native and cross-platform mobile apps for iOS and Android.", Icon = "fa-solid fa-mobile-screen", Image = "assets/images/resource/service-2.jpg", Features = ["iOS & Android", "Cross-Platform", "Offline Support", "Push Notifications"] },
        new ServiceDto { Id = 3, Slug = "cloud-solutions", Title = "Cloud Solutions", ShortDescription = "Scalable cloud infrastructure and migration services.", Icon = "fa-solid fa-cloud", Image = "assets/images/resource/service-3.jpg", Features = ["AWS & Azure", "Auto-Scaling", "Cost Optimization", "24/7 Monitoring"] },
        new ServiceDto { Id = 4, Slug = "cybersecurity", Title = "Cybersecurity", ShortDescription = "Comprehensive security audits and protection strategies.", Icon = "fa-solid fa-shield-halved", Image = "assets/images/resource/service-1.jpg", Features = ["Penetration Testing", "Compliance Audits", "Incident Response", "Security Training"] },
        new ServiceDto { Id = 5, Slug = "ai-solutions", Title = "AI & Machine Learning", ShortDescription = "Intelligent automation and data-driven insights.", Icon = "fa-solid fa-brain", Image = "assets/images/resource/service-2.jpg", Features = ["Predictive Analytics", "NLP Solutions", "Computer Vision", "Model Training"] },
        new ServiceDto { Id = 6, Slug = "it-consulting", Title = "IT Consulting", ShortDescription = "Strategic technology guidance for your business goals.", Icon = "fa-solid fa-handshake", Image = "assets/images/resource/service-3.jpg", Features = ["Digital Strategy", "Technology Roadmap", "Vendor Selection", "Risk Assessment"] },
    ];

    public Task<IEnumerable<ServiceDto>> GetServicesAsync() => Task.FromResult<IEnumerable<ServiceDto>>(_services);
    public Task<ServiceDto?> GetServiceBySlugAsync(string slug) =>
        Task.FromResult(_services.FirstOrDefault(s => s.Slug == slug));
}
