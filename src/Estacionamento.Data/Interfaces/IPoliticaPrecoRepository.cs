using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface IPoliticaPrecoRepository : IRepository<PoliticaPreco>
{
    Task<PoliticaPreco?> ObterByDataBaseAsync(DateTime dataBase);
    Task<IEnumerable<PeriodoLivre>> ListarPeriodosLivresAsync(int politicaPrecoId);
}