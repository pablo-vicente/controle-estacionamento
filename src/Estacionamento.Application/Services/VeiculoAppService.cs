using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Application.Services;

public class VeiculoAppService : IVeiculoAppService
{
    private readonly IVeiculoService _veiculoService;

    public VeiculoAppService(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public async Task CriarVeiculo(VeiculoRequest request)
    {
        await _veiculoService.CriarVeiculo(request);
    }

    public async Task EditarVeiculo(VeiculoRequest request)
    {
        await _veiculoService.EditarVeiculo(request);
    }
}