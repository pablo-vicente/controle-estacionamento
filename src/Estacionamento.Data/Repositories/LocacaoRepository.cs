using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;

namespace Estacionamento.Data.Repositories;

public class LocacaoRepository : ILocacaoRepository
{
    public void Adicionar(params Locacao[] locacoes)
    {
        throw new NotImplementedException();
    }

    public void Remover(params Locacao[] locacoes)
    {
        throw new NotImplementedException();
    }

    public void Atualizar(params Locacao[] locacoes)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Locacao> Listar()
    {
        throw new NotImplementedException();
    }

    public Locacao ObterById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Locacao> ListarByPlacaVeiculo(string placaVeiculo)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Locacao> ListarByCpfCondutor(string cpfCondutor)
    {
        throw new NotImplementedException();
    }

    public Locacao ObterLocacaoVeiculoEmAberto(string placaVeiculo)
    {
        throw new NotImplementedException();
    }
}