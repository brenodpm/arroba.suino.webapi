using System.Collections.Generic;
using arroba.suino.webapi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace arroba.suino.webapi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUseCase usuarios;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IUseCase usuarios, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            this.usuarios = usuarios;
        }

        [HttpGet]
        public IList<Domain.Entities.Usuario> Get()
        {
            return usuarios.GetUsuarios();
        }
    }
}
