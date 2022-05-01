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
    public void Registrar(LocacaoRequest request)
    {
        var condutor = _condutorRepository.ObterByCpf(request.CpfCondutor);
        var veiculo = _veiculoRepository.ObterByPlaca(request.PlacaVeiculo);
        
        // TODO VALIDAR DUAS LOCAOCES ABERTAS PARA MESMO VEICULO
        var locacao = new Locacao(DateTime.Now, null, veiculo.Id, condutor.Id);
        _locacaoRepository.Adicionar(locacao);
        _logger.LogInformation("Registrando locação");
    }

    public void Encerrar(string placaVeiculo)
    {
        var locacao = _locacaoRepository.ObterLocacaoVeiculoEmAberto(placaVeiculo);
        locacao.SetFim(DateTime.Now);
        locacao.SetStatus(Status.Fechado);
        
        var politicaPreco = _politicaPrecoRepository.ObterByDataBase(locacao.Inicio);
        var periodosLivre = (PeriodoLivre[])_politicaPrecoRepository.ListarPeriodosLivres(politicaPreco.Id);
        var condutor = _condutorRepository.ObterById(locacao.CondutoId);
        
        var precoLocacao = CalcularValorLocacao(locacao, politicaPreco, periodosLivre);
        var (temDesconto, precoComDesconto) = CalculoLocacaoService.CalcularValorLocacaoComDesconto(precoLocacao, politicaPreco, condutor);
        AtualizarDescontosCondutor(temDesconto, politicaPreco, condutor);

        _locacaoRepository.Atualizar(locacao);
        _condutorRepository.Atualizar(condutor);
        _logger.LogInformation("Encerrando locação");
    }

    private static void AtualizarDescontosCondutor(bool temDesconto, PoliticaPreco politicaPreco, Condutor condutor)
    {
        if (!temDesconto) 
            return;
        
        if (condutor.DescontosUtilizados + 1 < politicaPreco.QntDesconto) 
            return;
        condutor.SetDescontosUtilizados(0);
        condutor.SetTempoEstacionado(TimeSpan.Zero);
    }

    public static decimal CalcularValorLocacao(Locacao locacao, PoliticaPreco politicaPreco, params PeriodoLivre[] periodoLivres)
    {
        var tempoLivre = CalculoLocacaoService.CalcularTempoLivre(locacao, periodoLivres);
        var tempoLocaco = locacao.Fim.Value - locacao.Inicio - tempoLivre;
        var precoLocacaoAserPago = CalculoLocacaoService.CalcularValorSerPago(tempoLocaco, politicaPreco);
        
        return precoLocacaoAserPago;
    }

    

    
}