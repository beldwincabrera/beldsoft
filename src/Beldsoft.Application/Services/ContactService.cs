using Beldsoft.Application.DTOs;
using Beldsoft.Application.Interfaces;

namespace Beldsoft.Application.Services;

public class ContactService : IContactService
{
    public Task<bool> SubmitMessageAsync(ContactFormDto form)
    {
        // In production, send email or store in DB
        Console.WriteLine($"Contact from {form.Name} <{form.Email}>: {form.Message}");
        return Task.FromResult(true);
    }
}
