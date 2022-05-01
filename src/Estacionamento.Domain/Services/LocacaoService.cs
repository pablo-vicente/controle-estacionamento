using Estacionamento.Core.Dtos;
using Estacionamento.Core.Enums;
using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Domain.Services;

public class LocacaoService : ILocacaoService
{
    private readonly ILogger<LocacaoService> _logger;
    private readonly ICondutorRepository _condutorRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ILocacaoRepository _locacaoRepository;
    private readonly IPoliticaPrecoRepository _politicaPrecoRepository;

    public LocacaoService(
        ILogger<LocacaoService> logger,
        ICondutorRepository condutorRepository,
        IVeiculoRepository veiculoRepository,
        ILocacaoRepository locacaoRepository,
        IPoliticaPrecoRepository politicaPrecoRepository)
    {
        _logger = logger;
        _condutorRepository = condutorRepository;
        _veiculoRepository = veiculoRepository;
        _locacaoRepository = locacaoRepository;
        _politicaPrecoRepository = politicaPrecoRepository;
    }
    public async Task RegistrarAsync(LocacaoRequest request)
    {
        var condutor = await _condutorRepository.ObterByCpfAsync(request.CpfCondutor);
        var veiculo = await _veiculoRepository.ObterByPlacaAsync(request.PlacaVeiculo);
        var locacaoExistente = await _locacaoRepository.ListarAsync();
        
        if(locacaoExistente.Any(x => x.Status != Status.Fechado && x.VeiculoId == veiculo.Id))
            throw new InvalidOperationException("Já existe uma locação aberta para este veiculo");
        
        var novaLocacao = new Locacao(DateTime.Now, null, veiculo.Id, condutor.Id);
        await _locacaoRepository.AdicionarAsync(novaLocacao);
        _logger.LogInformation("Registrando locação");
    }

    public async Task<ResumoLocacaoResponse> EncerrarAsync(string placaVeiculo)
    {
        var veiculo = await _veiculoRepository.ObterByPlacaAsync(placaVeiculo);
        var locacao = (await _locacaoRepository.ListarAsync())
            .Where(x=>x.VeiculoId == veiculo.Id).
            MaxBy(x => x.Inicio);
            
        if(locacao is null || locacao.Status is Status.Fechado)
            throw new InvalidOperationException("Locação inexistente");
        
        locacao.SetFim(DateTime.Now);
        locacao.SetStatus(Status.Fechado);
        
        var politicaPreco = await _politicaPrecoRepository.ObterByDataBaseAsync(locacao.Inicio);
        
        if(politicaPreco is null)
            throw new InvalidOperationException("Locação inexistente");
        
        var periodosLivres = (PeriodoLivre[]) await _politicaPrecoRepository.ListarPeriodosLivresAsync(politicaPreco.Id);
        var condutor = await _condutorRepository.ObterByIdAsync(locacao.CondutorId);
        
        var resumoLocacao = CalcularResumoLocacao(locacao, periodosLivres, politicaPreco, condutor);
        AtualizarDescontosCondutor(resumoLocacao.TemDesconto, resumoLocacao.TempoEstacionadoCobrado, politicaPreco, condutor);
        
        await _locacaoRepository.AtualizarAsync(locacao);
        await _condutorRepository.AtualizarAsync(condutor);
        _logger.LogInformation("Encerrando locação");

        return resumoLocacao;
    }

    public static ResumoLocacaoResponse CalcularResumoLocacao(
        Locacao locacao, 
        PeriodoLivre[] periodosLivres, 
        PoliticaPreco politicaPreco,
        Condutor condutor)
    {
        var tempoLivre = CalculoLocacaoService.CalcularTempoLivre(locacao, periodosLivres);
        var tempoEstacionado = CalcularTempoEstacionado(locacao);
        var valorTotal = CalculoLocacaoService.CalcularValorSerPago(tempoEstacionado, politicaPreco);
        var valorTotalSemLivre = CalculoLocacaoService.CalcularValorSerPago(tempoEstacionado - tempoLivre, politicaPreco);
        var (temDesconto, valorComDesconto) = CalculoLocacaoService.CalcularValorLocacaoComDesconto(valorTotalSemLivre, politicaPreco, condutor);
        
        return new ResumoLocacaoResponse(
            tempoEstacionado,
            tempoLivre,
            valorTotal,
            politicaPreco.TaxaDesconto,
            valorComDesconto,
            temDesconto);
    }

    private static void AtualizarDescontosCondutor(bool temDesconto, TimeSpan novoTempoEstacionado, PoliticaPreco politicaPreco, Condutor condutor)
    {
        if (!temDesconto || condutor.DescontosUtilizados + 1 < politicaPreco.QntDesconto)
        {
            var tempoEstacionado =  condutor.TempoEstacionado + novoTempoEstacionado;
            condutor.SetTempoEstacionado(tempoEstacionado);
            return;
        }
        
        condutor.SetDescontosUtilizados(0);
        condutor.SetTempoEstacionado(TimeSpan.Zero);
    }

    private static TimeSpan CalcularTempoEstacionado(Locacao locacao) => locacao.Fim.Value - locacao.Inicio;
    
}