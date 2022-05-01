using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface ILocacaoRepository : IRepository<Locacao>
{
    Task<Locacao?> ObterLocacaoVeiculoEmAbertoAsync(int veiculoId);
}