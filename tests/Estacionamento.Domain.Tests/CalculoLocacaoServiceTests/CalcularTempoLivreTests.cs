using System;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Services;
using Xunit;

namespace Estacionamento.Domain.Tests;

public class CalcularTempoLivreTests
{
    [Theory]
    [InlineData(3, 10, 30, 3, 12, 00, 0, 0, 30)]
    [InlineData(3, 12, 00, 3, 14, 00, 0, 1, 0)]
    [InlineData(3, 11, 00, 3, 13, 30, 0, 1, 30)]
    [InlineData(3, 12, 00, 3, 13, 00, 0, 1, 00)]
    [InlineData(3, 10, 30, 4, 01, 00, 0, 1, 30)]
    [InlineData(3, 10, 30, 5, 01, 00, 0, 1, 30)]
    public void CalcularValorLocacao_PolitcaPrecosComPeriodoLivre_TempoLivreUtilizado(
        int diaInicio, int horaInicio, int minutoInicio , 
        int diaFim, int horaFim, int minutoFim,
        int diaEsperado, int horaEsperada, int minutoEsperado)
    {
        // Arrange
        var locacao = new Locacao(
            new DateTime(2022, 01, diaInicio, horaInicio, minutoInicio, 0),
            new DateTime(2022, 01, diaFim, horaFim, minutoFim, 0),
            1,
            1);

        var periodosLivres = new PeriodoLivre[]
        {
            new(
                DayOfWeek.Monday,
                new TimeSpan(11, 30, 0),
                new TimeSpan(13, 00, 00)),
            new(
                DayOfWeek.Wednesday,
                new TimeSpan(11, 30, 0),
                new TimeSpan(13, 00, 00)),
            new(
                DayOfWeek.Thursday,
                new TimeSpan(11, 30, 0),
                new TimeSpan(13, 00, 00))
        };
        
        // Act
        var valorLocacao = CalculoLocacaoService.CalcularTempoLivre(locacao, periodosLivres);

        // Asset
        Assert.Equal(new TimeSpan(diaEsperado, horaEsperada, minutoEsperado, 0), valorLocacao);
    }
}