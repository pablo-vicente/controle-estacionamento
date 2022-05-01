namespace Estacionamento.Core.Dtos;

public struct VeiculoRequest
{
    public VeiculoRequest(string placa, string cpf)
    {
        Placa = placa;
        Cpf = cpf;
    }

    public string Placa { get; }
    public string Cpf { get; }
}