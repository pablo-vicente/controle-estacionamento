using Estacionamento.Core.Dtos;

namespace Estacionamento.Domain.Interfaces;

public interface ICondutorService
{
    Task CriarCodutor(CondutorRequest request);
    
    Task EditarCodutor(CondutorRequest request);
}