using Estacionamento.Core.Dtos;
using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Domain.Services;

public class VeiculoService : IVeiculoService
{
    private readonly ILogger<VeiculoService> _logger;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ICondutorRepository _condutorRepository;

    public VeiculoService(
        ILogger<VeiculoService> logger,
        IVeiculoRepository veiculoRepository, 
        ICondutorRepository condutorRepository)
    {
        _logger = logger;
        _veiculoRepository = veiculoRepository;
        _condutorRepository = condutorRepository;
    }

    public async Task CriarVeiculo(VeiculoRequest request)
    {
        var condutor = await ObterCondutor(request);
        var veiculoExistente = await _veiculoRepository.ObterByPlacaAsync(request.Placa.Trim());

        if (veiculoExistente is not null)
            throw new InvalidOperationException($"Veiculo já cadastrado PLACA: {request.Placa.Trim()}");


        var veiculo = new Veiculo(request.Placa.Trim(), condutor.Id);
        await _veiculoRepository.AdicionarAsync(veiculo);
        _logger.LogInformation("Criando veiculo");
    }

    private async Task<Condutor> ObterCondutor(VeiculoRequest request)
    {
        var cpf = CondutorService.FormatarCpf(request.Cpf);
        var condutor = await _condutorRepository.ObterByCpfAsync(cpf);

        if (condutor is null)
            throw new InvalidOperationException("Codutor não cadastrado");
        return condutor;
    }

    public async Task EditarVeiculo(VeiculoRequest request)
    {
        var veiculoExistente = await _veiculoRepository.ObterByPlacaAsync(request.Placa.Trim());

        if (veiculoExistente is null)
            throw new InvalidOperationException($"Veiculo inexistente PLACA: {request.Placa.Trim()}");

        var condutor = await ObterCondutor(request);
        
        veiculoExistente.SetCondutorId(condutor.Id);
        await _veiculoRepository.AtualizarAsync(veiculoExistente);
    }
}