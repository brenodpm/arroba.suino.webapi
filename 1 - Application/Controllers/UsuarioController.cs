using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.DTO;
using arroba.suino.webapi.Domain.Interfaces.UseCase;
using arroba.suino.webapi.Infra.CrossCutting.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace arroba.suino.webapi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUseCaseExample usuarios;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUseCaseExample usuarios, ILogger<UsuarioController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            this.usuarios = usuarios;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            IList<UsuarioDTO> resp = _mapper.Map<List<UsuarioDTO>>(await usuarios.GetUsuarios());
            resp.ToList().ForEach(p =>
            {
                p.links.AddLink("get", Metodos.Name.Usuario, p.Id.ToString());
            });
            return Ok(resp);
        }
    }
}
