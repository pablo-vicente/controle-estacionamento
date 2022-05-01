using Estacionamento.Core.Dtos;

namespace Estacionamento.Domain.Interfaces;

public interface IPoliticaPrecoService
{
    Task CriarPoliticaPreco(PoliticaPrecoRequest request);
}