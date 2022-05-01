using Estacionamento.Core.Enums;

namespace Estacionamento.Data.Models;

public class Locacao
{
    public Locacao() { } // EF core

    public Locacao(DateTime inicio, DateTime? fim, int veiculoId, int condutorId)
    {
        Inicio = inicio;
        Fim = fim;
        Status = Status.EmAberto;
        VeiculoId = veiculoId;
        CondutorId = condutorId;
    }

    public int Id { get; }

    public DateTime Inicio { get;}
    public DateTime? Fim { get; private set; }

    public int VeiculoId { get;}
    public int CondutorId { get;}

    public Status Status { get; private set; }
    
    public void SetStatus(Status status)
    {
        Status = status;
    }
    public void SetFim(DateTime dateTime)
    {
        Fim = dateTime;
    }
}