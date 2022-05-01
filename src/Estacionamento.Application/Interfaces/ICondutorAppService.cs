using Estacionamento.Core.Dtos;

namespace Estacionamento.Application.Interfaces;

public interface ICondutorAppService
{
    Task CriarCodutor(CondutorRequest request);
    
    Task EditarCodutor(CondutorRequest request);
}