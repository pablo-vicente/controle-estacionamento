using Estacionamento.Core.Dtos;

namespace Estacionamento.Domain.Interfaces;

public interface ILocacaoService
{
    Task RegistrarAsync(LocacaoRequest request);
    Task<ResumoLocacaoResponse> EncerrarAsync(string placaVeiculo);
}