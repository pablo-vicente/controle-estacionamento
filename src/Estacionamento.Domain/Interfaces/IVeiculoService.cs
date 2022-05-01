using Estacionamento.Core.Dtos;

namespace Estacionamento.Domain.Interfaces;

public interface IVeiculoService
{
    Task CriarVeiculo(VeiculoRequest request);
    Task EditarVeiculo(VeiculoRequest request);
}