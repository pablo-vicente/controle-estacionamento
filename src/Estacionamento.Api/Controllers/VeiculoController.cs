using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : BaseController
    {
        private readonly ILogger<VeiculoController> _logger;
        private readonly IVeiculoAppService _veiculoAppService;
        public VeiculoController(ILogger<VeiculoController> logger, IVeiculoAppService veiculoAppService)
        {
            _logger = logger;
            _veiculoAppService = veiculoAppService;
        }
        public async Task<ActionResult> Criar([FromBody]VeiculoRequest request)
        {
            try
            {
                await _veiculoAppService.CriarVeiculo(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(TratarException(ex, _logger));
            }
        }
        
        public async Task<ActionResult> Editar([FromBody]VeiculoRequest request)
        {
            try
            {
                await _veiculoAppService.EditarVeiculo(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(TratarException(ex, _logger));
            }
        }
    }
}
