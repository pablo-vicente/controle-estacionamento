namespace Estacionamento.Data.Models;

public class Condutor
{
    public Condutor() { } // EF core

    public Condutor(string nome, string email, string cpf)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
        DescontosUtilizados = 0;
        TempoEstacionado = TimeSpan.Zero;
    }

    public int Id { get;}
    public string Nome { get; }
    public string Email { get; }
    public string Cpf { get; }
    public int DescontosUtilizados { get; private set; }

    public TimeSpan TempoEstacionado { get; private set; }
    
    public void SetDescontosUtilizados(int descontos) => DescontosUtilizados = descontos;
    public void SetTempoEstacionado(TimeSpan tempoEstacionado) => TempoEstacionado += tempoEstacionado;
}
