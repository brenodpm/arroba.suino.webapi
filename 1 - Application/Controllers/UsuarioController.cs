using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.DTO;
using arroba.suino.webapi.Domain.Interfaces.UseCase;
using arroba.suino.webapi.Infra.CrossCutting.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post()
        {
            string apikey = "breno";
            string secret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            string accessToken = "";
            string body = "deu certo";

            StringBuilder sbBody = new StringBuilder();
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(body);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sbBody.Append(hashBytes[i].ToString("X2"));
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();



            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = apikey,
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddSeconds(30),
                Claims = new Dictionary<string, object>{
                     {"jti", "nonce"},
                     {"accessToken", accessToken},
                     {"body", sbBody.ToString()}
                },
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
