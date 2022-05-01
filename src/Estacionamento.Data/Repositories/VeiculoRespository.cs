using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;

namespace Estacionamento.Data.Repositories;

public class VeiculoRespository : IVeiculoRepository
{
    public void Adicionar(params Veiculo[] veiculos)
    {
        throw new NotImplementedException();
    }

    public void Remover(params Veiculo[] veiculos)
    {
        throw new NotImplementedException();
    }

    public void Atualizar(params Veiculo[] veiculos)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Veiculo> Listar()
    {
        throw new NotImplementedException();
    }

    public Veiculo ObterById(int id)
    {
        throw new NotImplementedException();
    }

    public Veiculo ObterByPlaca(string placa)
    {
        throw new NotImplementedException();
    }
}