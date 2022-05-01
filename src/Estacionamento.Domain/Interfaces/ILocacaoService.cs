using Estacionamento.Core.Dtos;

namespace Estacionamento.Domain.Interfaces;

public interface ILocacaoService
{
    void Registrar(LocacaoRequest request);
    void Encerrar(string placaVeiculo);
}