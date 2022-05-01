using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Application.Services;

public class LocacaoAppService : ILocacaoAppService
{
    private readonly ILocacaoService _locacaoService;
    public LocacaoAppService(ILocacaoService locacaoService)
    {
        _locacaoService = locacaoService;
    }
    public async Task CriarLocacao(LocacaoRequest locacao)
    {
        await _locacaoService.CriarLocacaoAsync(locacao);
    }

    public async Task EncerrarrLocacao(string placaVeiculo)
    {
        await _locacaoService.EncerrarLocacaoAsync(placaVeiculo);
    }
}