namespace Estacionamento.Core.Dtos;

public struct CondutorRequest
{
    public CondutorRequest(string nome, string email, string cpf)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
    }

    public string Nome { get; }
    public string Email { get; }
    public string Cpf { get; }
}