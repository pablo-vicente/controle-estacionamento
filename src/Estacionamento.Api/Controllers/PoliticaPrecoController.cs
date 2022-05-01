using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliticaPrecoController : BaseController
    {
        private readonly ILogger<PoliticaPrecoController> _logger;
        private readonly IPoliticaPrecoAppService _politicaPrecoAppService;
        public PoliticaPrecoController(ILogger<PoliticaPrecoController> logger, IPoliticaPrecoAppService politicaPrecoAppService)
        {
            _logger = logger;
            _politicaPrecoAppService = politicaPrecoAppService;
        }
        public async Task<ActionResult> Criar([FromBody]PoliticaPrecoRequest request)
        {
            try
            {
                await _politicaPrecoAppService.CriarPoliticaPreco(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(TratarException(ex, _logger));
            }
        }
    }
}
