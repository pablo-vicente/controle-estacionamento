using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILogger<LocacaoController> _logger;
        private readonly ILocacaoAppService _locacaoAppService;
        public LocacaoController(ILogger<LocacaoController> logger, ILocacaoAppService locacaoAppService)
        {
            _logger = logger;
            _locacaoAppService = locacaoAppService;
        }
        public ActionResult Registrar([FromBody]LocacaoRequest locacao)
        {
            try
            {
                _locacaoAppService.Registrar(locacao);
                return Ok();
            }
            catch (Exception e)
            {
                // TODO CENTRALIZAR
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                return BadRequest("Ocorreu um erro");
            }
        }
    }
}
