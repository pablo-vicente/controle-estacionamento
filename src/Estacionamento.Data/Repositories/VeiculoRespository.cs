using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data.Repositories;

public class VeiculoRespository : IVeiculoRepository
{
    private readonly EstacionamentoDbContext _estacionamentoDbContext;

    public VeiculoRespository(EstacionamentoDbContext estacionamentoDbContext)
    {
        _estacionamentoDbContext = estacionamentoDbContext;
    }

    public async Task AdicionarAsync(params Veiculo[] veiculos)
    {
        await _estacionamentoDbContext.Veiculos.AddRangeAsync(veiculos);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task RemoverAsync(params Veiculo[] veiculos)
    {
        _estacionamentoDbContext.Veiculos.RemoveRange(veiculos);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public async Task AtualizarAsync(params Veiculo[] veiculos)
    {
        _estacionamentoDbContext.Veiculos.UpdateRange(veiculos);
        await _estacionamentoDbContext.SaveChangesAsync();
    }

    public IQueryable<Veiculo> Listar()
    {
        return _estacionamentoDbContext.Veiculos;
    }

    public async Task<Veiculo?> ObterByIdAsync(int id)
    {
        return await _estacionamentoDbContext.Veiculos.FindAsync(id);
    }

    public async Task<Veiculo?> ObterByPlacaAsync(string placa)
    {
        return await _estacionamentoDbContext.Veiculos.FirstOrDefaultAsync(x=>x.Placa.Equals(placa, StringComparison.InvariantCultureIgnoreCase));
    }
}