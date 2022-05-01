using System.ComponentModel.DataAnnotations;
using Estacionamento.Core.Dtos;
using Estacionamento.Data.Interfaces;
using Estacionamento.Data.Models;
using Estacionamento.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Estacionamento.Domain.Services;

public class CondutorService : ICondutorService
{
    private readonly ILogger<CondutorService> _logger;
    private readonly ICondutorRepository _condutorRepository;

    public CondutorService(
        ILogger<CondutorService> logger,
        ICondutorRepository condutorRepository)
    {
        _logger = logger;
        _condutorRepository = condutorRepository;
    }
    
    public async Task CriarCodutor(CondutorRequest request)
    {
        var requestValidad = ValidarCadastroCondutor(request);

        var condutor = await _condutorRepository.ObterByCpfAsync(requestValidad.Cpf);
        if (condutor is not null)
            throw new InvalidOperationException($"Condutor já cadastrado CPF:{requestValidad.Cpf}");

        var novoCondutor = new Condutor(requestValidad.Nome, requestValidad.Email, requestValidad.Cpf);

        await _condutorRepository.AdicionarAsync(novoCondutor);
        _logger.LogInformation("Saldo registro condutor");
    }

    private static CondutorRequest ValidarCadastroCondutor(CondutorRequest request)
    {
        var cpf = FormatarCpf(request.Cpf);
        var email = request.Email.Trim();

        var emailValido = new EmailAddressAttribute().IsValid(email);
        if (!emailValido)
            throw new InvalidOperationException($"Email invalido");
        return new CondutorRequest(request.Nome.Trim(), email, cpf);
    }

    public async Task EditarCodutor(CondutorRequest request)
    {
        var requestValidad = ValidarCadastroCondutor(request);
        
        var condutor = await _condutorRepository.ObterByCpfAsync(request.Cpf);
        if (condutor is null)
            throw new InvalidOperationException("Codutor Inexistente");
        
        condutor.SetNome(requestValidad.Nome);
        condutor.SetEmail(requestValidad.Email);
        await _condutorRepository.AtualizarAsync(condutor);
    }

    public static string FormatarCpf(string cpf)
    {
        try
        {
            var cpfLimpo = cpf
                .Replace(".", string.Empty)
                .Replace("-", string.Empty)
                .Trim()
                .PadLeft(13, '0');

            var cpfFormatado = Convert.ToInt64(cpfLimpo)
                .ToString(@"00\.000\.000\/0000\-00");

            return cpfFormatado;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("CPF invalido");
        }
    }
}