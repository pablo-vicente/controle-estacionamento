
namespace Estacionamento.Data.Interfaces;

public interface IRepository<T>
{
    Task AdicionarAsync(params T[] t);
    Task RemoverAsync(params T[] t);
    Task AtualizarAsync(params T[] t);
    IQueryable<T> Listar();
    Task<T?> ObterByIdAsync(int id);
}