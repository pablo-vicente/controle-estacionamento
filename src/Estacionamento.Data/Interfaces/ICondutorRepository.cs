using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface ICondutorRepository : IRepository<Condutor>
{
    Task<Condutor?> ObterByCpfAsync(string cpf);
}