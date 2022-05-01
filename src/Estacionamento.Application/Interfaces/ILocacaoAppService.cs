using Estacionamento.Core.Dtos;

namespace Estacionamento.Application.Interfaces;

public interface ILocacaoAppService
{
    Task CriarLocacao(LocacaoRequest locacao);
    Task EncerrarrLocacao(string placaVeiculo);
}