namespace Estacionamento.Core.Dtos;

public struct ResumoLocacaoResponse
{
    public ResumoLocacaoResponse(TimeSpan tempoEstacionado, TimeSpan tempoLivre, decimal valorTotal, decimal taxaDesconto, decimal valorComDescontos, bool temDesconto)
    {
        TempoEstacionado = tempoEstacionado;
        TempoLivre = tempoLivre;
        ValorTotal = valorTotal;
        TaxaDesconto = taxaDesconto;
        ValorComDescontos = valorComDescontos;
        TemDesconto = temDesconto;
    }

    public TimeSpan TempoEstacionadoCobrado => TempoEstacionado - TempoLivre;
    public TimeSpan TempoEstacionado { get;}
    public TimeSpan TempoLivre { get;}
    public decimal ValorTotal { get;}
    public decimal TaxaDesconto { get;}
    public decimal ValorComDescontos { get;}
    public bool TemDesconto { get; }
}