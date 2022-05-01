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

    public IQueryable<Locacao> Listar()
    {
        return _estacionamentoDbContext.Locacoes.AsQueryable();
    }

    public async Task<Locacao?> ObterByIdAsync(int id)
    {
        return await _estacionamentoDbContext.Locacoes.FindAsync(id);
    }

    public async Task<Locacao?> ObterLocacaoVeiculoEmAbertoAsync(int veiculoId)
    {
        return await _estacionamentoDbContext.Locacoes.FirstOrDefaultAsync(x => x.VeiculoId == veiculoId);
    }
}