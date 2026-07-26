using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class FaqService : IFaqService
{
    private static readonly IReadOnlyList<FaqItemDto> Faqs =
    [
        new() { Id = 1, Category = "Engagement", Question = "What kinds of businesses does Beldsoft work with?", Answer = "Beldsoft is positioned for small and medium-sized organizations that need experienced software engineering, architecture, integration, automation, or technical leadership without building a large internal engineering department." },
        new() { Id = 2, Category = "Engagement", Question = "Can you improve an existing application instead of replacing it?", Answer = "Yes. Modernization should start with an assessment of the current system, business risk, and highest-value constraints. A staged improvement plan is often safer and more economical than a full rewrite." },
        new() { Id = 3, Category = "Process", Question = "How does a consulting engagement begin?", Answer = "We begin with a focused consultation to understand the business problem, users, current systems, constraints, and desired outcome. If there is a fit, the next step is a clearly scoped discovery or delivery proposal." },
        new() { Id = 4, Category = "Process", Question = "How will scope, timing, and cost be determined?", Answer = "Those details depend on the problem and the level of uncertainty. Beldsoft defines assumptions, deliverables, responsibilities, and commercial terms in a written proposal before delivery begins." },
        new() { Id = 5, Category = "Technical", Question = "Can Beldsoft integrate with systems we already use?", Answer = "Integration is a core service. Feasibility depends on the systems involved, the APIs or data access they provide, security requirements, and vendor constraints. Discovery makes those dependencies explicit before implementation." },
        new() { Id = 6, Category = "Technical", Question = "How do you approach AI projects?", Answer = "We start with the business workflow and measurable value, then evaluate whether AI is appropriate. Implementations should include data protection, human oversight, error handling, and clear limits rather than treating AI as an automatic solution." },
        new() { Id = 7, Category = "Support", Question = "Can Beldsoft support software after launch?", Answer = "Ongoing maintenance, improvement, and technical guidance can be included when the required service level and responsibilities are defined in the engagement." }
    ];

    public Task<IEnumerable<FaqItemDto>> GetFaqItemsAsync() =>
        Task.FromResult<IEnumerable<FaqItemDto>>(Faqs);

    public Task<IEnumerable<string>> GetCategoriesAsync() =>
        Task.FromResult(Faqs.Select(item => item.Category).Distinct());
}
