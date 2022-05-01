using Estacionamento.Core.Dtos;

namespace Estacionamento.Application.Interfaces;

public interface ILocacaoAppService
{
    void Registrar(LocacaoRequest locacao);
}