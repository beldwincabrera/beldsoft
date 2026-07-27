using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class TestimonialService : ITestimonialService
{
    public Task<IEnumerable<TestimonialDto>> GetTestimonialsAsync() =>
        Task.FromResult<IEnumerable<TestimonialDto>>([]);
}
