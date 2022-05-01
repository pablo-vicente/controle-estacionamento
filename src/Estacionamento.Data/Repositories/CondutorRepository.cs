using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data.Repositories;

public class CondutorRepository: ICondutorRepository
{
    private readonly EstacionamentoDbContext _estacionamentoDbContext;

    public CondutorRepository(EstacionamentoDbContext estacionamentoDbContext)
    {
        _estacionamentoDbContext = estacionamentoDbContext;
    }

    public async Task AdicionarAsync(params Condutor[] codutor)
    {
        await _estacionamentoDbContext.Condutores.AddRangeAsync(codutor);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task RemoverAsync(params Condutor[] codutor)
    {
        _estacionamentoDbContext.Condutores.RemoveRange(codutor);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task AtualizarAsync(params Condutor[] codutor)
    {
        _estacionamentoDbContext.Condutores.UpdateRange(codutor);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public IQueryable<Condutor> Listar()
    {
        return _estacionamentoDbContext.Condutores;
    }

    public async Task<Condutor?> ObterByIdAsync(int id)
    {
        return await _estacionamentoDbContext.Condutores.FindAsync(id) ?? null;
    }

    public async Task<Condutor?> ObterByCpfAsync(string cpf)
    {
        return await _estacionamentoDbContext.Condutores.FirstOrDefaultAsync(x=>x.Cpf.Equals(cpf));
    }
}