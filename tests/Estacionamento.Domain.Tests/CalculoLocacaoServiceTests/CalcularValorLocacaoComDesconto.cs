using System;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Services;
using Xunit;

namespace Estacionamento.Domain.Tests.CalculoLocacaoServiceTests;

public class CalcularDescontoTests
{
    //MetodoEmTeste_EstadoEmTeste_ComportamentoEsperado
    [Theory]
    [InlineData(100, 0, 9, 0, false, 100)]
    [InlineData(100, 2, 9, 0, false, 100)]
    [InlineData(100, 0, 10, 0, true, 50)]
    [InlineData(100, 1, 10, 0, true, 50)]
    [InlineData(100, 2, 10, 0, false, 100)]
    public void CalcularDesconto_QtnDescontoHoras_DescontoCorreto(
        decimal valorSerPago, 
        int descontosUtilizados, 
        int horaEstacionada, int minutosEstacionados,
        bool temDescontoEsperado, decimal precoEsperado)
    {
        // Arrange
        var politicaPreco = new PoliticaPreco(
            DateTime.Now,
            new DateTime(2022, 01, 01),
            new DateTime(2022, 12, 31),
            2,
            1,
            30,
            10,
            10,
            50,
            2);
        
        var tempoEstacionado = new TimeSpan(horaEstacionada, minutosEstacionados, 0);
        var condutor = new Condutor(
            "Joao",
            "0000",
            "000");
        condutor.SetDescontosUtilizados(descontosUtilizados);
        condutor.SetTempoEstacionado(tempoEstacionado);
        
        // Act
        var precoComDesconto = CalculoLocacaoService.CalcularValorLocacaoComDesconto(valorSerPago, politicaPreco, condutor);

        // Assert
        Assert.Equal((temDescontoEsperado, precoEsperado), precoComDesconto);
    }
}