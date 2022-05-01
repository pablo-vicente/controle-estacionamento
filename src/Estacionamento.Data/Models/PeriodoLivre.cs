namespace Estacionamento.Data.Models;

public class PeriodoLivre
{
    public PeriodoLivre() { } // EF core

    public PeriodoLivre(DayOfWeek diaSemana, TimeSpan inicio, TimeSpan fim, int politicaPrecoId)
    {
        DiaSemana = diaSemana;
        Inicio = inicio;
        Fim = fim;
        PoliticaPrecoId = politicaPrecoId;
    }

    public int Id { get; private set; }
    public DayOfWeek DiaSemana { get;}
    public TimeSpan Inicio { get;}
    public TimeSpan Fim { get;}

    public int PoliticaPrecoId { get; set; }
}