namespace Estacionamento.Data.Interfaces;

public interface IRepository<T>
{
    void Adicionar(params T[] t);
    void Remover(params T[] t);
    void Atualizar(params T[] t);
    IEnumerable<T> Listar();
    T ObterById(int id);
}