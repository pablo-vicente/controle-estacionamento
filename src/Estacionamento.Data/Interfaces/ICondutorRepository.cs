using Estacionamento.Data.Models;

namespace Estacionamento.Data.Interfaces;

public interface ICondutorRepository : IRepository<Condutor>
{
    Condutor ObterByCpf(string cpf);
}