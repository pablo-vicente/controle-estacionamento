using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CondutorController : BaseController
    {
        private readonly ILogger<CondutorController> _logger;
        private readonly ICondutorAppService _condutorAppService;
        public CondutorController(ILogger<CondutorController> logger, ICondutorAppService condutorAppService)
        {
            _logger = logger;
            _condutorAppService = condutorAppService;
        }
        
        [HttpPost]
        public async Task<ActionResult> Criar([FromBody]CondutorRequest request)
        {
            try
            {
                await _condutorAppService.CriarCodutor(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(TratarException(ex, _logger));
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Editar([FromBody]CondutorRequest request)
        {
            try
            {
                await _condutorAppService.EditarCodutor(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(TratarException(ex, _logger));
            }
        }
    }
}
