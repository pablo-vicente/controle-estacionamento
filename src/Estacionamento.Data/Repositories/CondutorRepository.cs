using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;

namespace Estacionamento.Data.Repositories;

public class CondutorRepository: ICondutorRepository
{
    public void Adicionar(params Condutor[] codutor)
    {
        throw new NotImplementedException();
    }

    public void Remover(params Condutor[] codutor)
    {
        throw new NotImplementedException();
    }

    public void Atualizar(params Condutor[] codutor)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Condutor> Listar()
    {
        throw new NotImplementedException();
    }

    public Condutor ObterById(int id)
    {
        throw new NotImplementedException();
    }

    public Condutor ObterByCpf(string cpf)
    {
        throw new NotImplementedException();
    }
}