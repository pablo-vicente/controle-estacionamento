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

    public async Task<IEnumerable<PoliticaPreco>> ListarAsync()
    {
        return await _estacionamentoDbContext.PoliticasPrecos.ToArrayAsync();
    }

    public async Task<PoliticaPreco> ObterByIdAsync(int id)
    {
        var politicaPreco = await _estacionamentoDbContext.PoliticasPrecos.FindAsync(id);
        if (politicaPreco is null)
            throw new ApplicationException($"Politica de Preco não encontrada ID: {id}");
        return politicaPreco;
    }

    public async Task<PoliticaPreco> ObterByDataBaseAsync(DateTime dataBase)
    {
        var politicaPreco = await _estacionamentoDbContext.PoliticasPrecos.FirstOrDefaultAsync(x=>x.DataBase == dataBase);
        if (politicaPreco is null)
            throw new ApplicationException($"Locação não encontrada DATA BASE:: {dataBase:dd/MM-yyyy}");

        return politicaPreco;
    }

    public async Task<IEnumerable<PeriodoLivre>> ListarPeriodosLivresAsync(int politicaPrecoId)
    {
        return await _estacionamentoDbContext.PeriodosLivres.Where(x=>x.PoliticaPrecoId == politicaPrecoId).ToListAsync();
    }
}