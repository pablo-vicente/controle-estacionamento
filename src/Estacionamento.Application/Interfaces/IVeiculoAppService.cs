using Estacionamento.Core.Dtos;

namespace Estacionamento.Application.Interfaces;

public interface IVeiculoAppService
{
    Task CriarVeiculo(VeiculoRequest request);
    
    Task EditarVeiculo(VeiculoRequest request);
}