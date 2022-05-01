using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface ILocacaoRepository : IRepository<Locacao>
{
    IEnumerable<Locacao> ListarByPlacaVeiculo(string placaVeiculo);
    IEnumerable<Locacao> ListarByCpfCondutor(string cpfCondutor);
    Locacao ObterLocacaoVeiculoEmAberto(string placaVeiculo);
}