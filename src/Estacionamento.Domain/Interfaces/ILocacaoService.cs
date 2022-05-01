using Estacionamento.Core.Dtos;

namespace Estacionamento.Domain.Interfaces;

public interface ILocacaoService
{
    Task CriarLocacaoAsync(LocacaoRequest request);
    Task<ResumoLocacaoResponse> EncerrarLocacaoAsync(string placaVeiculo);
}