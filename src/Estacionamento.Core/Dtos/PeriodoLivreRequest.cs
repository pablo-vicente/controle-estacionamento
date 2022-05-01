namespace Estacionamento.Core.Dtos;

public struct PeriodoLivreRequest
{
    public PeriodoLivreRequest(DayOfWeek diaSemana, TimeSpan inicio, TimeSpan fim)
    {
        DiaSemana = diaSemana;
        Inicio = inicio;
        Fim = fim;
    }

    public DayOfWeek DiaSemana { get;}
    public TimeSpan Inicio { get;}
    public TimeSpan Fim { get;}
}