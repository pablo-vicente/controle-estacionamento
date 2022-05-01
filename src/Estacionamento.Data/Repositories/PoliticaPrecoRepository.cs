using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;

namespace Estacionamento.Data.Repositories;

public class PoliticaPrecoRepository : IPoliticaPrecoRepository
{
    public void Adicionar(params PoliticaPreco[] t)
    {
        throw new NotImplementedException();
    }

    public void Remover(params PoliticaPreco[] t)
    {
        throw new NotImplementedException();
    }

    public void Atualizar(params PoliticaPreco[] t)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PoliticaPreco> Listar()
    {
        throw new NotImplementedException();
    }

    public PoliticaPreco ObterById(int id)
    {
        throw new NotImplementedException();
    }

    public PoliticaPreco ObterByDataBase(DateTime dataBase)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PeriodoLivre> ListarPeriodosLivres(int politicaPrecoId)
    {
        throw new NotImplementedException();
    }
}