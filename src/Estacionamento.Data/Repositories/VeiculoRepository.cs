using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly EstacionamentoDbContext _estacionamentoDbContext;

    public VeiculoRepository(EstacionamentoDbContext estacionamentoDbContext)
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

    public async Task<IEnumerable<Veiculo>> ListarAsync()
    {
        return await _estacionamentoDbContext.Veiculos.ToListAsync();
    }

    public async Task<Veiculo> ObterByIdAsync(int id)
    {
        var veiculo = await _estacionamentoDbContext.Veiculos.FindAsync(id);
        if (veiculo is null)
            throw new ApplicationException($"Veiculo não encontrada ID: {id}");

        return veiculo;
    }

    public async Task<Veiculo?> ObterByPlacaAsync(string placa)
    {
        return await _estacionamentoDbContext.Veiculos.FirstOrDefaultAsync(x=>x.Placa.Equals(placa, StringComparison.InvariantCultureIgnoreCase));;
    }
}