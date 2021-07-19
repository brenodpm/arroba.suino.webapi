using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.Domain.Exceptions;
using arroba.suino.webapi.Interfaces.Repository;
using arroba.suino.webapi.Interfaces.UseCase;
using Microsoft.IdentityModel.Tokens;

namespace arroba.suino.webapi.Service.UseCase
{
    public class AccessTokenService : IAccessTokenService
    {
        private const string SESSAO_EXPIRADA = "Sessão expirada";
        private readonly ISessaoRepository sessaoRepository;
        private readonly IEmpresaRepository empresaRepository;
        private readonly IGrupoRepository grupoRepository;

        public AccessTokenService(ISessaoRepository sessaoRepository, IEmpresaRepository empresaRepository, IGrupoRepository grupoRepository)
        {
            this.sessaoRepository = sessaoRepository;
            this.empresaRepository = empresaRepository;
            this.grupoRepository = grupoRepository;
        }

        public async Task Validar(string accessToken, string claimExigida)
        {
            Guid codSessao = ExtrairCodSessao(accessToken);
            Sessao sessao = await sessaoRepository.Select(codSessao);
            if (sessao == null || !sessao.Ativo)
            {
                throw new AccessTokenServiceException(SESSAO_EXPIRADA);
            }

            Empresa empresa = await empresaRepository.Select(sessao.CodEmpresa);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(empresa.Security.ToString());
            try
            {
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);
            }
            catch
            {
                throw new AccessTokenServiceException(SESSAO_EXPIRADA);
            }

            Grupo grupo = await grupoRepository.Select(sessao.CodGrupo);
            if (grupo == null || !grupo.Ativo)
            {
                throw new AccessTokenServiceException(SESSAO_EXPIRADA);
            }

            if (!grupo.Permissoes.Contains(claimExigida))
            {
                throw new AccessTokenServiceException();
            }
        }

        private static Guid ExtrairCodSessao(string token)
        {
            JwtSecurityToken jwt = ExtrairJwt(token);

            Guid cod = new Guid(jwt.Issuer);
            return cod;
        }


        private static JwtSecurityToken ExtrairJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            return tokenS;
        }
    }
}