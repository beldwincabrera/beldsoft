using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface IFaqService
{
    Task<IEnumerable<FaqItemDto>> GetFaqItemsAsync();
    Task<IEnumerable<string>> GetCategoriesAsync();
}
