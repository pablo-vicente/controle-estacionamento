using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Application.Services;

public class CondutorAppService : ICondutorAppService
{
    private readonly ICondutorService _condutorService;

    public CondutorAppService(ICondutorService condutorService)
    {
        _condutorService = condutorService;
    }

    public async Task CriarCodutor(CondutorRequest request)
    {
        await _condutorService.CriarCodutor(request);
    }

    public async Task EditarCodutor(CondutorRequest request)
    {
        await _condutorService.EditarCodutor(request);
    }
}