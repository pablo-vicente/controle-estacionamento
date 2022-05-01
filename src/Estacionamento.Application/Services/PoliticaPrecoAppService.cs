using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Application.Services;

public class PoliticaPrecoAppService : IPoliticaPrecoAppService
{
    private readonly IPoliticaPrecoService _politicaPrecoService;

    public PoliticaPrecoAppService(IPoliticaPrecoService politicaPrecoService)
    {
        _politicaPrecoService = politicaPrecoService;
    }

    public async Task CriarPoliticaPreco(PoliticaPrecoRequest request)
    {
        await _politicaPrecoService.CriarPoliticaPreco(request);
    }
}