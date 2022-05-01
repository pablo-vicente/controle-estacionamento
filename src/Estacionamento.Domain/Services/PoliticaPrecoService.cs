using Estacionamento.Core.Dtos;
using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Domain.Services;

public class PoliticaPrecoService : IPoliticaPrecoService
{
    public const string ErrosValidacao = "ERROS_VALIDACAO";
    private readonly ILogger<PoliticaPrecoService> _logger;
    private readonly IPoliticaPrecoRepository _politicaPrecoRepository;

    public PoliticaPrecoService(
        ILogger<PoliticaPrecoService> logger, 
        IPoliticaPrecoRepository politicaPrecoRepository)
    {
        _logger = logger;
        _politicaPrecoRepository = politicaPrecoRepository;
    }


    public async Task CriarPoliticaPreco(PoliticaPrecoRequest request)
    {
        ValidarPoliticaPreco(request);

        var periodosLivres = request.PeriodosLivres
            .Select(x => new PeriodoLivre(x.DiaSemana, x.Inicio, x.Fim));
        
        var politicaPreco = new PoliticaPreco(DateTime.Now,
            request.InicioVigencia,
            request.FimVigencia,
            request.ValorHora,
            request.ValorAdicionalHora,
            request.TempoMinimo,
            request.Tolerancia,
            request.HorasDesconto,
            request.TaxaDesconto,
            request.QntLocacoesDesconto,
            periodosLivres);

        await _politicaPrecoRepository.AdicionarAsync(politicaPreco);
        _logger.LogInformation("Criando Politica de Preco");

    }

    private static void ValidarPoliticaPreco(PoliticaPrecoRequest request)
    {
        var mensageNegativo = "Não é pertido valores negativos";
        var erros = new List<string>();
        if (request.InicioVigencia > request.FimVigencia)
            erros.Add("Inicio da vigencia nao pode ser maior que fim");

        if (request.ValorHora < 0)
            erros.Add(mensageNegativo + " " + "para Valor da Hora");

        if (request.ValorAdicionalHora < 0)
            erros.Add(mensageNegativo + " " + "para Valor Adicional da Hora");

        if (request.TempoMinimo < 0)
            erros.Add(mensageNegativo + " " + "para Tempo Mínimo");

        if (request.Tolerancia < 0)
            erros.Add(mensageNegativo + " " + "para Tolerânca");

        if (request.HorasDesconto < 0)
            erros.Add(mensageNegativo + " " + "para Horas Acumuladas para Desconto");

        if (request.TaxaDesconto < 0)
            erros.Add(mensageNegativo + " " + "para Taxa Desconto");

        if (request.QntLocacoesDesconto < 0)
            erros.Add(mensageNegativo + " " + "para Taxa Desconto");

        erros.AddRange(from requestPeriodosLivre in request.PeriodosLivres
            where requestPeriodosLivre.Inicio > requestPeriodosLivre.Fim
            select $"Hora de Incio no pode ser maior que a hora de fim para dia {requestPeriodosLivre.DiaSemana}");

        if (!erros.Any())
            return;
        
        var exception = new InvalidOperationException("Foram encontratos erros no preenchimento");
        exception.Data.Add(ErrosValidacao, erros);
        throw exception;

    }
}