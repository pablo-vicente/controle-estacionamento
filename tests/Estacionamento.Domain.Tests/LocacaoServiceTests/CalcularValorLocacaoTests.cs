using System;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Services;
using Xunit;

namespace Estacionamento.Domain.Tests.LocacaoServiceTests;

public class CalcularValorLocacaoTests
{
    [Theory]
    [InlineData(3, 10, 30, 3, 12, 00, 2)]
    [InlineData(3, 12, 00, 3, 14, 00, 2)]
    [InlineData(3, 11, 00, 3, 13, 30, 2)]
    [InlineData(3, 12, 00, 3, 13, 00, 0)]
    [InlineData(3, 10, 30, 4, 01, 00, 14)]
    [InlineData(3, 10, 30, 5, 01, 00, 38)]
    public void CalcularValorLocacao_PolitcaPrecosComPeriodoLivre_ValorLocaco(
        int diaInicio, int horaInicio, int minutoInicio , 
        int diaFim, int horaFim, int minutoFim,
        decimal valorEsperado)
    {
        // Arrange
        var locacao = new Locacao(
            new DateTime(2022, 01, diaInicio, horaInicio, minutoInicio, 0),
            new DateTime(2022, 01, diaFim, horaFim, minutoFim, 0),
            1,
            1);
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
    
        var periodosLivres = new PeriodoLivre[]
        {
            new(
                DayOfWeek.Monday,
                new TimeSpan(11, 30, 0),
                new TimeSpan(13, 00, 00),
                1),
            new(
                DayOfWeek.Wednesday,
                new TimeSpan(11, 30, 0),
                new TimeSpan(13, 00, 00),
                1),
            new(
                DayOfWeek.Thursday,
                new TimeSpan(11, 30, 0),
                new TimeSpan(13, 00, 00),
                1)
        };
        
        // Act
        var valorLocacao = LocacaoService.CalcularValorLocacao(locacao, politicaPreco, periodosLivres);
    
        // Asset
        Assert.Equal(valorEsperado, valorLocacao);
    }
}