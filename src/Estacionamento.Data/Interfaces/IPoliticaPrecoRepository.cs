using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface IPoliticaPrecoRepository : IRepository<PoliticaPreco>
{
    PoliticaPreco ObterByDataBase(DateTime dataBase);
    IEnumerable<PeriodoLivre> ListarPeriodosLivres(int politicaPrecoId);
}