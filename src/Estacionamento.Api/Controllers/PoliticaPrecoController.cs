using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Api.Controllers
{
    [Route("api/politica-preco/[action]")]
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
        [HttpPost]
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
