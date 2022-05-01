namespace Estacionamento.Data.Models;

public class PoliticaPreco
{
    public PoliticaPreco() { } // EF core
    

    public PoliticaPreco(
        DateTime dataBase, 
        DateTime inicioVigencia, 
        DateTime fimVigencia, 
        decimal valorHora, 
        decimal valorAdicionalHora, 
        decimal tempoMinimo, 
        int tolerancia, 
        int horasDesconto, 
        int taxaDesconto, 
        int qntLocacoesDesconto,
        IEnumerable<PeriodoLivre> periodosLivres)
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

    public int Id { get;}
    
    /// <summary>
    /// Data Cadastro do Preco
    /// </summary>
    public DateTime DataBase { get; }
    
    /// <summary>
    /// Inicio da Validade
    /// </summary>
    public DateTime InicioVigencia { get; }
    
    /// <summary>
    /// Fim da Validade
    /// </summary>
    public DateTime FimVigencia { get; }
    
    public decimal ValorHora { get; }
    public decimal ValorAdicionalHora { get; }
    public decimal TempoMinimo { get; }

    /// <summary>
    /// Em minutos
    /// </summary>
    public int Tolerancia { get; }

    public int HorasDesconto { get;}
    public int TaxaDesconto { get;}
    public int QntLocacoesDesconto { get;}

    public IEnumerable<PeriodoLivre> PeriodosLivres { get; }

}