using System;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Services;
using Xunit;

namespace Estacionamento.Domain.Tests.CalculoLocacaoServiceTests;

public class CalcularValorSerPagoTests
{
    //MetodoEmTeste_EstadoEmTeste_ComportamentoEsperado
    [Theory]
    [InlineData(0, 10, 1)]
    [InlineData(0, 30, 1)]
    [InlineData(1, 0, 2)]
    [InlineData(1, 10, 2)]
    [InlineData(1, 15, 3)]
    [InlineData(2, 5, 3)]
    [InlineData(2, 15, 4)]
    public void CalcularValorLocacao_PolitcaPrecosSemPeriodoLivre_ValorLocaco(int horas, int minutos , decimal valorEsperado)
    {
        // Arrange
        var tempoLocacao = new TimeSpan(horas, minutos, 0);
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
        
        // Act
        var valorLocacao = CalculoLocacaoService.CalcularValorSerPago(tempoLocacao, politicaPreco);

        // Asset
        Assert.Equal(valorEsperado, valorLocacao);
    }
}