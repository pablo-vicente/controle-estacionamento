using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface IVeiculoRepository : IRepository<Veiculo>
{
    Task<Veiculo?> ObterByPlacaAsync(string placa);
}