namespace Estacionamento.Core.Dtos;

public struct LocacaoRequest
{
    public LocacaoRequest(string placaVeiculo, string cpfCondutor)
    {
        PlacaVeiculo = placaVeiculo;
        CpfCondutor = cpfCondutor;
    }

    public string PlacaVeiculo { get; private set; }
    public string CpfCondutor { get; private set; }
}