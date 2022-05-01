using Estacionamento.Data.Models;

namespace Estacionamento.Domain.Services;

public static class CalculoLocacaoService
{
    public static decimal CalcularValorSerPago(TimeSpan tempoLocaco, PoliticaPreco politicaPreco)
    {
        // 30 minutos valor R$ 1,00            | 
        // 1 hora valor R$ 2,00                |
        // 1 hora 10 minutos valor R$ 2,00     | 
        // 1 hora e 15 minutos valor R$ 3,00   | 
        // 2 horas e 5 minutos valor R$ 3,00   | 
        // 2 horas e 15 minutos valor R$ 4,00. |

        if (tempoLocaco.TotalMinutes is 0)
            return 0;
        
        var precoFracionario = politicaPreco.ValorHora / 2;
        var totalMinutos = (int)tempoLocaco.TotalMinutes;
        var totalHoras = (int)tempoLocaco.TotalHours;
        
        if (totalMinutos <= politicaPreco.TempoMinimo)
            return precoFracionario;

        var precoLocacaoAserPago = politicaPreco.ValorHora;
        precoLocacaoAserPago += (totalHoras - 1) * precoFracionario;
        
        if (totalMinutos - totalHoras * 60 > politicaPreco.Tolerancia)
            precoLocacaoAserPago += precoFracionario;
        
        return precoLocacaoAserPago;
    }
    
    public static TimeSpan CalcularTempoLivre(Locacao locacao, params PeriodoLivre[] periodoLivres)
    {
        var intervalos = new HashSet<(DateTime, DateTime)>();
        
        if (locacao.Fim.Value.Date == locacao.Inicio.Date)
            intervalos.Add((locacao.Inicio, locacao.Fim.Value));
        else
        {
            var diaInicio = (locacao.Inicio, locacao.Inicio.Date.AddDays(1).AddMinutes(-1));
            var diaFim = (locacao.Fim.Value.Date, locacao.Fim.Value);
            intervalos.Add(diaInicio);
            intervalos.Add(diaFim);

            if ((locacao.Fim.Value.Date - locacao.Inicio.Date).TotalDays > 1)
            {
                var meio = (locacao.Inicio.Date.AddDays(1), locacao.Fim.Value.Date.AddMinutes(-1));
                intervalos.Add(meio);
            }
        }
        
        var tempoLivre = CalcularHorasLivresIntervalos(intervalos, periodoLivres);

        return tempoLivre;
    }

    private static TimeSpan CalcularHorasLivresIntervalos(HashSet<(DateTime, DateTime)> intervalos, params PeriodoLivre[] periodoLivres)
    {
        var horas = 0;
        var minutos = 0;
        
        foreach (var (inicioPeriodo, fimPeriodo) in intervalos)
        {
            var data = inicioPeriodo;
            do
            {
                var periodoLivreInicio = periodoLivres.FirstOrDefault(x => x.DiaSemana == data.DayOfWeek);

                if (periodoLivreInicio is not null)
                {
                    var inicioHoraLivre = data.Date
                        .AddHours(periodoLivreInicio.Inicio.Hours)
                        .AddMinutes(periodoLivreInicio.Inicio.Minutes);

                    var fimHoraLivre = data.Date
                        .AddHours(periodoLivreInicio.Fim.Hours)
                        .AddMinutes(periodoLivreInicio.Fim.Minutes);

                    var horaInicio = data.Date.AddHours(inicioPeriodo.Hour).AddMinutes(inicioPeriodo.Minute);
                    var horaFim = data.Date.AddHours(fimPeriodo.Hour).AddMinutes(fimPeriodo.Minute);
                    if (horaInicio <= fimHoraLivre && horaFim >= inicioHoraLivre)
                    {
                        var inicioConsideradoLivre = inicioHoraLivre;
                        var fimConsideradoLivre = fimHoraLivre;

                        if (horaInicio > inicioHoraLivre)
                            inicioConsideradoLivre = horaInicio;

                        if (horaFim < fimHoraLivre)
                            fimConsideradoLivre = horaFim;

                        var tempoLivre = fimConsideradoLivre - inicioConsideradoLivre;
                        horas += tempoLivre.Hours;
                        minutos += tempoLivre.Minutes;
                    }
                }

                data = data.AddDays(1);
            } while (data <= fimPeriodo);
        }

        return new TimeSpan(horas, minutos, 0);;
    }
    
    public static (bool, decimal) CalcularValorLocacaoComDesconto(decimal precoLocacao, PoliticaPreco politicaPreco, Condutor condutor)
    {
        if (!TemDesconto(politicaPreco, condutor)) 
            return (false, precoLocacao);
        
        var temDesconto = true;
        var precoComDesconto =  precoLocacao * (1 - politicaPreco.TaxaDesconto / 100m);

        return (temDesconto, precoComDesconto);
    }
    
    private static bool TemDesconto(PoliticaPreco politicaPreco, Condutor condutor)
    {
        if (condutor.TempoEstacionado.TotalHours < politicaPreco.HorasDesconto)
            return false;

        return condutor.DescontosUtilizados < politicaPreco.QntDesconto;
    }
}