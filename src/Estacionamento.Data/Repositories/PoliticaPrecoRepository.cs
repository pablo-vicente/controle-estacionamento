using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data.Repositories;

public class PoliticaPrecoRepository : IPoliticaPrecoRepository
{
    private readonly EstacionamentoDbContext _estacionamentoDbContext;

    public PoliticaPrecoRepository(EstacionamentoDbContext estacionamentoDbContext)
    {
        _estacionamentoDbContext = estacionamentoDbContext;
    }

    public async Task AdicionarAsync(params PoliticaPreco[] politicaPrecos)
    {
        await _estacionamentoDbContext.PoliticasPrecos.AddRangeAsync(politicaPrecos);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task RemoverAsync(params PoliticaPreco[] politicaPrecos)
    {
        _estacionamentoDbContext.PoliticasPrecos.RemoveRange(politicaPrecos);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task AtualizarAsync(params PoliticaPreco[] politicaPrecos)
    {
        _estacionamentoDbContext.PoliticasPrecos.UpdateRange(politicaPrecos);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public IQueryable<PoliticaPreco> Listar()
    {
        return _estacionamentoDbContext.PoliticasPrecos;
    }

    public async Task<PoliticaPreco?> ObterByIdAsync(int id)
    {
        return await _estacionamentoDbContext.PoliticasPrecos.FindAsync(id);
    }

    public async Task<PoliticaPreco?> ObterByDataBaseAsync(DateTime dataBase)
    {
        return await _estacionamentoDbContext.PoliticasPrecos.FirstOrDefaultAsync(x=>x.DataBase == dataBase);
    }

    public async Task<IEnumerable<PeriodoLivre>> ListarPeriodosLivresAsync(int politicaPrecoId)
    {
        return await _estacionamentoDbContext.PeriodosLivres.Where(x=>x.PoliticaPrecoId == politicaPrecoId).ToListAsync();
    }
}