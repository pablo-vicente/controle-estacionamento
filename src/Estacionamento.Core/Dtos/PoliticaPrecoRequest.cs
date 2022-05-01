namespace Estacionamento.Core.Dtos;

public struct PoliticaPrecoRequest
{
    public PoliticaPrecoRequest(DateTime dataBase,
     DateTime inicioVigencia,
     DateTime fimVigencia,
     decimal valorHora,
     decimal valorAdicionalHora,
     int tempoMinimo,
     int tolerancia,
     int horasDesconto,
     int taxaDesconto,
     int qntLocacoesDesconto,
     IEnumerable<PeriodoLivreRequest> periodosLivres)
    {
        DataBase = dataBase;
        InicioVigencia = inicioVigencia;
        FimVigencia = fimVigencia;
        ValorHora = valorHora;
        ValorAdicionalHora = valorAdicionalHora;
        TempoMinimo = tempoMinimo;
        Tolerancia = tolerancia;
        HorasDesconto = horasDesconto;
        TaxaDesconto = taxaDesconto;
        QntLocacoesDesconto = qntLocacoesDesconto;
        PeriodosLivres = periodosLivres;
    }

    public DateTime DataBase { get; }
    public DateTime InicioVigencia { get; }
    public DateTime FimVigencia { get; }
    public decimal ValorHora { get; }
    public decimal ValorAdicionalHora { get; }
    public int TempoMinimo { get; }
    public int Tolerancia { get; }
    public int HorasDesconto { get; }
    public int TaxaDesconto { get; }
    public int QntLocacoesDesconto { get; }

    public IEnumerable<PeriodoLivreRequest> PeriodosLivres;
}