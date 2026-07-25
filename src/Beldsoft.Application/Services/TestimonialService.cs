using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class TestimonialService : ITestimonialService
{
    private static readonly List<TestimonialDto> _testimonials =
    [
        new TestimonialDto { Id = 1, ClientName = "Technology Executive", ClientTitle = "Executive sponsor", ClientCompany = "Financial services", ClientImage = "assets/images/resource/author-1.jpg", Quote = "The team brought structure to a complex modernization effort and made the tradeoffs understandable to both technical and business stakeholders.", Rating = 5 },
        new TestimonialDto { Id = 2, ClientName = "Product Leader", ClientTitle = "Product sponsor", ClientCompany = "Healthcare technology", ClientImage = "assets/images/resource/author-2.jpg", Quote = "Beldsoft asked the right questions early, kept delivery transparent, and helped us reach a production-ready solution with confidence.", Rating = 5 },
        new TestimonialDto { Id = 3, ClientName = "Engineering Director", ClientTitle = "Technical sponsor", ClientCompany = "Retail technology", ClientImage = "assets/images/resource/author-3.jpg", Quote = "Their engineers worked as part of our team, improved the architecture, and left us with clearer operational practices and documentation.", Rating = 5 },
        new TestimonialDto { Id = 4, ClientName = "Operations Leader", ClientTitle = "Business sponsor", ClientCompany = "Professional services", ClientImage = "assets/images/resource/author-4.jpg", Quote = "Communication was direct and consistent. We always understood progress, risks, and what decisions were needed from us.", Rating = 5 },
    ];

    public Task<IEnumerable<TestimonialDto>> GetTestimonialsAsync() =>
        Task.FromResult<IEnumerable<TestimonialDto>>(_testimonials);
}
