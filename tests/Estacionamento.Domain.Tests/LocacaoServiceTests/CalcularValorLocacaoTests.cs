using System;
using System.Collections.Generic;
using Estacionamento.Core.Dtos;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Estacionamento.Domain.Tests.LocacaoServiceTests;

public class CalcularValorLocacaoTests
{
    public static IEnumerable<object[]> Locacoes()
    {
        yield return new object[]
        {
            new Locacao(
            new DateTime(2022, 01, 3, 10, 30, 0),
            new DateTime(2022, 01,  3, 12, 00, 0),
            1,
            1),
            new ResumoLocacaoResponse(
                new TimeSpan(1, 30, 00),
                new TimeSpan(0, 30, 0),
                3,
                50m,
                2, 
                false)
        };
        yield return new object[]
        {
            new Locacao(
                new DateTime(2022, 01, 3, 12, 00, 0),
                new DateTime(2022, 01,  3, 14, 00, 0),
                1,
                1),
            new ResumoLocacaoResponse(
                new TimeSpan(2, 0, 0),
                new TimeSpan(1, 00, 0),
                3,
                50m,
                2, 
                false)
        };
        yield return new object[]
        {
            new Locacao(
                new DateTime(2022, 01, 3, 11, 00, 0),
                new DateTime(2022, 01,  3, 13, 30, 0),
                1,
                1),
            new ResumoLocacaoResponse(
                new TimeSpan(2, 30, 0),
                new TimeSpan(1, 30, 0),
                4,
                50m,
                2, 
                false)
        };
        yield return new object[]
        {
            new Locacao(
                new DateTime(2022, 01, 3, 12, 00, 0),
                new DateTime(2022, 01,   3, 13, 00, 0),
                1,
                1),
            new ResumoLocacaoResponse(
                new TimeSpan(1, 0, 0),
                new TimeSpan(1, 0, 0),
                2,
                50m,
                0, 
                false)
        };
        yield return new object[]
        {
            new Locacao(
                new DateTime(2022, 01, 3, 10, 30, 0),
                new DateTime(2022, 01,  4, 01, 00, 0),
                1,
                1),
            new ResumoLocacaoResponse(
                new TimeSpan(14, 30, 0),
                new TimeSpan(1, 30, 0),
                16,  //13h:30 + 1h
                50m,
                14, 
                false)
        };
        yield return new object[]
        {
            new Locacao(
                new DateTime(2022, 01, 3, 10, 30, 0),
                new DateTime(2022, 01,  5, 01, 00, 0),
                1,
                1),
            new ResumoLocacaoResponse(
                new TimeSpan(1, 14, 30, 0),
                new TimeSpan(1, 30, 0),
                40, // 13h30m + 24h + 1h
                50m,
                38, 
                false)
        };
    } 
    
    [Theory]
    [MemberData(nameof(Locacoes))]
    public void CalcularValorLocacao_PolitcaPrecosComPeriodoLivre_ValorLocaco(
        Locacao locacao,
        ResumoLocacaoResponse locacaoResponseEsperado)
    {
        // Arrange
        var condutor = new Condutor();
        
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
            2,
            periodosLivres);
        
        // Act
        var valorLocacao = LocacaoService.CalcularResumoLocacao(locacao, politicaPreco, condutor);
    
        // Asset
        valorLocacao.Should().BeEquivalentTo(locacaoResponseEsperado);
    }
}