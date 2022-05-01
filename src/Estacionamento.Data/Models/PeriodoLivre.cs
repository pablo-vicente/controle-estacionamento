namespace Estacionamento.Data.Models;

public class PeriodoLivre
{
    public PeriodoLivre() { } // EF core

    public PeriodoLivre(DayOfWeek diaSemana, TimeSpan inicio, TimeSpan fim, int vigenciaPrecoId)
    {
        DiaSemana = diaSemana;
        Inicio = inicio;
        Fim = fim;
        VigenciaPrecoId = vigenciaPrecoId;
    }

    public int Id { get; private set; }
    public DayOfWeek DiaSemana { get;}
    public TimeSpan Inicio { get;}
    public TimeSpan Fim { get;}

    public int VigenciaPrecoId { get; set; }
}