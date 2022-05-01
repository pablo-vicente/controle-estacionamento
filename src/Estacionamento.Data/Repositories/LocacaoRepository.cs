using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data.Repositories;

public class LocacaoRepository : ILocacaoRepository
{
    private readonly EstacionamentoDbContext _estacionamentoDbContext;

    public LocacaoRepository(EstacionamentoDbContext estacionamentoDbContext)
    {
        _estacionamentoDbContext = estacionamentoDbContext;
    }

    public async Task AdicionarAsync(params Locacao[] locacoes)
    {
        await _estacionamentoDbContext.Locacoes.AddRangeAsync(locacoes);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task RemoverAsync(params Locacao[] locacoes)
    {
        _estacionamentoDbContext.Locacoes.RemoveRange(locacoes);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task AtualizarAsync(params Locacao[] locacoes)
    {
        _estacionamentoDbContext.Locacoes.UpdateRange(locacoes);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Locacao>> ListarAsync()
    {
        return await _estacionamentoDbContext.Locacoes.ToListAsync();
    }

    public async Task<Locacao> ObterByIdAsync(int id)
    {
        var locacao = await _estacionamentoDbContext.Locacoes.FindAsync(id);
        if (locacao is null)
            throw new ApplicationException($"Locação não encontrada: {id}");
        
        return locacao;
    }
}