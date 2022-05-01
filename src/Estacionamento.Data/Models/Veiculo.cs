namespace Estacionamento.Data.Models;

public class Veiculo
{
    public Veiculo() { } // EF core

    public Veiculo(string placa, int condutorId)
    {
        Placa = placa;
    }

    public int Id { get; }
    public string Placa { get;}
    public int CondutorId { get; set; }
    
}