namespace Estacionamento.Data.Models;

public class PeriodoLivre
{
    public PeriodoLivre() { } // EF core

    public PeriodoLivre(DayOfWeek diaSemana, TimeSpan inicio, TimeSpan fim)
    {
        DiaSemana = diaSemana;
        Inicio = inicio;
        Fim = fim;
    }

    public int Id { get; private set; }
    public DayOfWeek DiaSemana { get;}
    public TimeSpan Inicio { get;}
    public TimeSpan Fim { get;}

    public virtual PoliticaPreco PoliticaPreco{ get; }
}