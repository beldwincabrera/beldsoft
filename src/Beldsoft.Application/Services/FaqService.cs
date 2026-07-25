using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class FaqService : IFaqService
{
    private static readonly List<FaqItemDto> _faqs =
    [
        new FaqItemDto { Id = 1, Category = "General", Question = "What kinds of work does Beldsoft take on?", Answer = "We design and build custom software, modernize existing applications, improve cloud and delivery platforms, develop AI-enabled workflows, and advise engineering leaders on architecture and execution." },
        new FaqItemDto { Id = 2, Category = "General", Question = "Can you work with an existing product and engineering team?", Answer = "Yes. We can own a defined workstream, provide specialist capability, or work alongside your team to improve architecture, delivery practices, and product outcomes." },
        new FaqItemDto { Id = 3, Category = "Pricing", Question = "How is an engagement priced?", Answer = "Pricing depends on outcomes, scope, risk, team composition, and timeline. After an initial conversation, we recommend an engagement model and provide a transparent proposal with assumptions." },
        new FaqItemDto { Id = 4, Category = "Pricing", Question = "Do you support both projects and ongoing partnerships?", Answer = "Yes. Engagements can begin with a focused discovery sprint, continue as a defined delivery project, or operate as an ongoing engineering partnership." },
        new FaqItemDto { Id = 5, Category = "Technical", Question = "What technologies do you work with?", Answer = "Our work commonly includes .NET and Blazor, modern JavaScript frameworks, Azure and AWS, containers, Kubernetes, Python, relational and document databases, and production AI platforms." },
        new FaqItemDto { Id = 6, Category = "Technical", Question = "What happens after launch?", Answer = "We plan for operations during delivery and can provide monitoring, reliability improvements, incident support, security updates, and continued product development after launch." },
        new FaqItemDto { Id = 7, Category = "Process", Question = "What does your delivery process look like?", Answer = "We align on measurable outcomes, reduce the highest risks early, work in short delivery cycles, demonstrate progress frequently, and release incrementally with quality and operational readiness built in." },
        new FaqItemDto { Id = 8, Category = "Process", Question = "How will we stay informed?", Answer = "You receive a clear delivery cadence, direct access to the team, regular demonstrations, visible decisions and risks, and concise reporting tailored to your stakeholders." },
    ];

    public Task<IEnumerable<FaqItemDto>> GetFaqItemsAsync() =>
        Task.FromResult<IEnumerable<FaqItemDto>>(_faqs);

    public Task<IEnumerable<string>> GetCategoriesAsync() =>
        Task.FromResult(_faqs.Select(f => f.Category).Distinct());
}
