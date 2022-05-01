
namespace Estacionamento.Data.Interfaces;

public interface IRepository<T>
{
    Task AdicionarAsync(params T[] t);
    Task RemoverAsync(params T[] t);
    Task AtualizarAsync(params T[] t);
    Task<IEnumerable<T>> ListarAsync();
    Task<T> ObterByIdAsync(int id);
}