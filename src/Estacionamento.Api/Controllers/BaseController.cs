using Estacionamento.Application.Interfaces;
using Estacionamento.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public string TratarException(Exception ex, ILogger logger)
        {
            switch (ex)
            {
                case InvalidOperationException:
                    return "Ocorreu um erro inesperado! Contate o suporte!";
                default:
                    logger.LogError(ex.GetType().ToString());
                    logger.LogError(ex.Message);
                    logger.LogError(ex.StackTrace);
                    return ex.Message;
            }
        }
    }
}
