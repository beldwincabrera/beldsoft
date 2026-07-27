using Beldsoft.Application.DTOs;

namespace Beldsoft.Application.Interfaces;

public interface ILeadService
{
    Task<bool> SubmitLeadAsync(LeadDto lead);
}
