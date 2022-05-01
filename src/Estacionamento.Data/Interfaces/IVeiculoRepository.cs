using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface IVeiculoRepository : IRepository<Veiculo>
{
    Veiculo ObterByPlaca(string placa);
}