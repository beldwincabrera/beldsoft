using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface ITestimonialService
{
    Task<IEnumerable<TestimonialDto>> GetTestimonialsAsync();
}
