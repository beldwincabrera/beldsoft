using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class FaqService : IFaqService
{
    private static readonly List<FaqItemDto> _faqs =
    [
        new FaqItemDto { Id = 1, Category = "General", Question = "What services does Beldsoft offer?", Answer = "Beldsoft provides a full range of IT services including web development, mobile applications, cloud solutions, cybersecurity, AI/ML, and strategic IT consulting." },
        new FaqItemDto { Id = 2, Category = "General", Question = "How long has Beldsoft been in business?", Answer = "Beldsoft has been delivering technology solutions since 2015, with over 200 successful projects across various industries." },
        new FaqItemDto { Id = 3, Category = "Pricing", Question = "How is pricing determined for custom projects?", Answer = "Custom project pricing is based on scope, timeline, and technology requirements. We provide detailed proposals after an initial discovery call." },
        new FaqItemDto { Id = 4, Category = "Pricing", Question = "Do you offer monthly retainer agreements?", Answer = "Yes, we offer flexible monthly retainer plans for ongoing development, maintenance, and support services." },
        new FaqItemDto { Id = 5, Category = "Technical", Question = "What technologies do you specialize in?", Answer = "Our core stack includes .NET/Blazor, React, Flutter, Azure, AWS, and Kubernetes. We also work with Python, Node.js, and various database technologies." },
        new FaqItemDto { Id = 6, Category = "Technical", Question = "Do you provide ongoing support after project delivery?", Answer = "Absolutely. We offer tiered support packages including bug fixes, performance monitoring, feature updates, and 24/7 emergency response." },
        new FaqItemDto { Id = 7, Category = "Process", Question = "What is your typical project delivery process?", Answer = "We follow an agile methodology: Discovery → Architecture → Development sprints → Testing → Deployment → Support. Clients receive regular updates throughout." },
        new FaqItemDto { Id = 8, Category = "Process", Question = "How do you handle project communication?", Answer = "We assign a dedicated project manager and use tools like Jira, Slack, and weekly video calls to keep all stakeholders aligned." },
    ];

    public Task<IEnumerable<FaqItemDto>> GetFaqItemsAsync() =>
        Task.FromResult<IEnumerable<FaqItemDto>>(_faqs);

    public Task<IEnumerable<string>> GetCategoriesAsync() =>
        Task.FromResult(_faqs.Select(f => f.Category).Distinct());
}
