using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Estacionamento.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Application.Services;

public class LocacaoAppService : ILocacaoAppService
{
    private readonly ILogger<LocacaoAppService> _logger;
    private readonly ILocacaoService _locacaoService;
    public LocacaoAppService(ILogger<LocacaoAppService> logger, ILocacaoService locacaoService)
    {
        _logger = logger;
        _locacaoService = locacaoService;
    }
    public void Registrar(LocacaoRequest locacao)
    {
        _locacaoService.CriarLocacaoAsync(locacao);
    }
}