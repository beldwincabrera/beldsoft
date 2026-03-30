using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class TestimonialService : ITestimonialService
{
    private static readonly List<TestimonialDto> _testimonials =
    [
        new TestimonialDto { Id = 1, ClientName = "Robert Chen", ClientTitle = "CEO", ClientCompany = "TechVentures", ClientImage = "assets/images/resource/testi-1.jpg", Quote = "Beldsoft transformed our digital infrastructure. Their expertise in cloud solutions saved us 40% on operational costs while tripling our system reliability.", Rating = 5 },
        new TestimonialDto { Id = 2, ClientName = "Amanda Foster", ClientTitle = "CTO", ClientCompany = "HealthFirst", ClientImage = "assets/images/resource/testi-2.jpg", Quote = "The team delivered our healthcare platform on time and exceeded every performance benchmark. Truly exceptional work.", Rating = 5 },
        new TestimonialDto { Id = 3, ClientName = "David Park", ClientTitle = "Director of IT", ClientCompany = "Global Retail", ClientImage = "assets/images/resource/testi-3.jpg", Quote = "Their cybersecurity audit identified critical vulnerabilities we didn't know existed. We sleep better at night thanks to Beldsoft.", Rating = 5 },
        new TestimonialDto { Id = 4, ClientName = "Lisa Morgan", ClientTitle = "Product Manager", ClientCompany = "FinanceHub", ClientImage = "assets/images/resource/testi-1.jpg", Quote = "Outstanding communication throughout the project. The AI analytics dashboard they built has become our most used internal tool.", Rating = 4 },
    ];

    public Task<IEnumerable<TestimonialDto>> GetTestimonialsAsync() =>
        Task.FromResult<IEnumerable<TestimonialDto>>(_testimonials);
}
