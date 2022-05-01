using Estacionamento.Core.Dtos;

namespace Estacionamento.Application.Interfaces;

public interface IPoliticaPrecoAppService
{
    Task CriarPoliticaPreco(PoliticaPrecoRequest request);
}