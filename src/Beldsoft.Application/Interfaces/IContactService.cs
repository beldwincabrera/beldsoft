using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface IContactService
{
    Task<bool> SubmitMessageAsync(ContactFormDto form);
}
