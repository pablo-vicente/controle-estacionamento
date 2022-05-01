namespace Estacionamento.Data.Models;

public class PoliticaPreco
{
    public PoliticaPreco() { } // EF core
    

    public PoliticaPreco(DateTime dataBase, DateTime inicioVigencia, DateTime fimVigencia, 
        decimal valorHora, decimal valorAdicionalHora, decimal tempoMinimo, 
        int tolerancia, 
        int horasDesconto, int taxaDesconto, int qntDesconto)
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
        QntDesconto = qntDesconto;
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

    public int HorasDesconto { get; set; }
    public int TaxaDesconto { get; set; }
    public int QntDesconto { get; set; }
    
}