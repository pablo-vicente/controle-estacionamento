using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocacaoController : BaseController
    {
        private readonly ILogger<LocacaoController> _logger;
        private readonly ILocacaoAppService _locacaoAppService;
        public LocacaoController(ILogger<LocacaoController> logger, ILocacaoAppService locacaoAppService)
        {
            _logger = logger;
            _locacaoAppService = locacaoAppService;
        }
        
        [HttpPost]
        public async Task<ActionResult> Criar([FromBody]LocacaoRequest locacao)
        {
            try
            {
                await _locacaoAppService.CriarLocacao(locacao);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(TratarException(ex, _logger));
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Encerrar([FromBody]string placaVeiculo)
        {
            try
            {
                await _locacaoAppService.EncerrarrLocacao(placaVeiculo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(TratarException(ex, _logger));
            }
        }
    }
}
