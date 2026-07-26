using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public sealed class BlogService : IBlogService
{
    private static readonly IReadOnlyList<BlogPostDto> Posts =
    [
        new()
        {
            Id = 1,
            Slug = "when-custom-software-makes-business-sense",
            Title = "When custom software makes business sense",
            Excerpt = "A practical framework for deciding whether to build, buy, integrate, or improve the process first.",
            Author = "Beldsoft",
            PublishedAt = new DateTime(2026, 7, 24),
            Categories = ["Software Strategy"],
            Tags = ["Custom Software", "Decision Framework"],
            Content = """
                <p>Custom software is not automatically the best answer to an inefficient process. It becomes the right investment when the way your business operates creates an advantage, the available products cannot support that work without serious compromise, or repeated manual effort is already costing more than a focused solution.</p>
                <h2>Start with the business constraint</h2>
                <p>Before discussing technology, define where work slows down, who is affected, how often the problem occurs, and what the current workaround costs. A useful problem statement is measurable: it describes lost time, avoidable risk, delayed revenue, poor visibility, or an experience that prevents customers or employees from completing important work.</p>
                <h2>Compare four realistic options</h2>
                <p>Most organizations should evaluate process improvement, an off-the-shelf product, integration between existing tools, and custom development. Improving the process is usually the least expensive option. Buying is often fastest when the workflow is standard. Integration is effective when the necessary capabilities already exist but information is fragmented. Custom software is strongest when the workflow is distinctive and materially important.</p>
                <h2>Look beyond the initial build</h2>
                <p>The decision must include ownership, support, security, hosting, data migration, user adoption, and future change. A smaller solution that the organization can operate confidently is more valuable than an ambitious platform that becomes difficult to maintain.</p>
                <h2>A responsible next step</h2>
                <p>Run a focused discovery before committing to implementation. Document the current process, desired outcome, constraints, options, and success measures. The result should be a decision and a staged roadmap—not a predetermined justification for writing code.</p>
                """
        },
        new()
        {
            Id = 2,
            Slug = "modernize-legacy-software-without-big-bang-rewrite",
            Title = "Modernize legacy software without betting on a big-bang rewrite",
            Excerpt = "Reduce operational risk in controlled stages while preserving the business capabilities that still work.",
            Author = "Beldsoft",
            PublishedAt = new DateTime(2026, 7, 17),
            Categories = ["Modernization"],
            Tags = ["Legacy Systems", "Architecture"],
            Content = """
                <p>A full rewrite can appear cleaner than improving an aging system, but it concentrates cost, uncertainty, and operational risk into one initiative. Incremental modernization creates opportunities to validate assumptions and deliver value before the entire replacement is complete.</p>
                <h2>Assess the system before choosing the solution</h2>
                <p>Document business-critical workflows, dependencies, data ownership, failure patterns, security concerns, release constraints, and areas where change is unusually difficult. Separate architectural problems from process, skills, or operational issues. Not every problem requires replacement.</p>
                <h2>Stabilize what the business depends on</h2>
                <p>Improve monitoring, backups, deployment repeatability, automated testing around critical behavior, and incident visibility. These investments reduce immediate exposure and make later modernization safer.</p>
                <h2>Create seams for gradual change</h2>
                <p>Introduce clear interfaces around high-value capabilities, then move or replace them one at a time. APIs, events, and well-defined data boundaries can allow old and new components to coexist while users continue working.</p>
                <h2>Measure progress in business terms</h2>
                <p>Track release frequency, incident impact, processing time, support effort, and the lead time required for important changes. Modernization is successful when the system becomes safer and easier to evolve—not simply when the technology stack is newer.</p>
                """
        },
        new()
        {
            Id = 3,
            Slug = "practical-ai-automation-for-small-business",
            Title = "A practical filter for AI and workflow automation",
            Excerpt = "Identify useful automation opportunities without forcing AI into work that simpler technology can handle better.",
            Author = "Beldsoft",
            PublishedAt = new DateTime(2026, 7, 10),
            Categories = ["Automation & AI"],
            Tags = ["Applied AI", "Workflow Automation"],
            Content = """
                <p>The strongest automation opportunities are usually repetitive, high-volume, rule-guided, and connected to a clear operational outcome. AI can extend automation when the work involves unstructured text, documents, classification, or recommendations—but it also introduces uncertainty that must be managed.</p>
                <h2>Choose the workflow before the technology</h2>
                <p>Map the current steps, inputs, decisions, exceptions, and handoffs. Look for duplicate entry, waiting, inconsistent decisions, and information that must be copied between systems. Quantify the frequency and cost of the problem before building a solution.</p>
                <h2>Use the simplest reliable capability</h2>
                <p>Deterministic rules are preferable when the decision can be expressed clearly. Use AI where it contributes a capability that rules cannot provide economically, such as extracting meaning from varied documents or producing a draft for review.</p>
                <h2>Design for review and failure</h2>
                <p>Define confidence thresholds, human approval points, audit records, access controls, and what happens when a model or external service is unavailable. Sensitive or consequential decisions should not silently depend on an unverified output.</p>
                <h2>Prove value with a narrow pilot</h2>
                <p>Begin with one workflow and a limited user group. Compare time, quality, rework, and exception rates against the original process. Expand only after the evidence supports the operational and financial case.</p>
                """
        }
    ];

    public Task<IEnumerable<BlogPostSummaryDto>> GetPostsAsync(int page = 1, int pageSize = 9)
    {
        var safePage = Math.Max(1, page);
        var safePageSize = Math.Clamp(pageSize, 1, 50);
        return Task.FromResult<IEnumerable<BlogPostSummaryDto>>(
            Posts.Skip((safePage - 1) * safePageSize).Take(safePageSize));
    }

    public Task<BlogPostDto?> GetPostBySlugAsync(string slug) =>
        Task.FromResult(Posts.FirstOrDefault(post =>
            string.Equals(post.Slug, slug, StringComparison.OrdinalIgnoreCase)));

    public Task<IEnumerable<BlogPostSummaryDto>> GetRecentPostsAsync(int count = 3) =>
        Task.FromResult<IEnumerable<BlogPostSummaryDto>>(Posts.Take(Math.Max(0, count)));

    public Task<IEnumerable<string>> GetCategoriesAsync() =>
        Task.FromResult<IEnumerable<string>>(Posts.SelectMany(post => post.Categories).Distinct());

    public Task<int> GetTotalCountAsync() => Task.FromResult(Posts.Count);
}
