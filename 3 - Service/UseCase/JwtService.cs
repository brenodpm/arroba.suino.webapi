using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.Domain.Exceptions;
using arroba.suino.webapi.Interfaces.Repository;
using arroba.suino.webapi.Interfaces.UseCase;
using Microsoft.IdentityModel.Tokens;

namespace arroba.suino.webapi.Service.UseCases
{
    public class JwtService : IJwtService
    {
        private const string JWT_NAO_INFORMADO = "JWT não informado";
        private const string TERMINAL_INVALIDO = "Terminal de acesso inválido";
        private const string BODY_CLAIM = "body";
        private const string BODY_DIFERENTE = "O corpo da mensagem não condiz com o cabeçalho informado";

        private readonly IClienteRepository clienteRepository;

        public JwtService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public void ValidarJwtComBody(string token, string body)
        {
            if (token == null)
            {
                throw new JwtServiceException(JWT_NAO_INFORMADO);
            }

            ValidarAssinatura(token);
            ValidarBody(token, body);
        }

        private static void ValidarBody(string token, string body)
        {
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
            string hash = ExtrairBodyHash(token);
            if (!sbBody.ToString().Equals(hash))
            {
                throw new JwtServiceException(BODY_DIFERENTE);
            }
        }

        private void ValidarAssinatura(string token)
        {
            Cliente cliente = BuscarCliente(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(cliente.ApiSecret.ToString());
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);
            }
            catch
            {
                throw new JwtServiceException(TERMINAL_INVALIDO);
            }
        }

        private Cliente BuscarCliente(string token)
        {
            Guid apikey = ExtrairApiKey(token);

            Cliente cliente = clienteRepository.GetByApiKey(apikey);

            if (cliente == null)
            {
                throw new JwtServiceException(TERMINAL_INVALIDO);
            }

            if (!cliente.Ativo)
            {
                throw new JwtServiceException(TERMINAL_INVALIDO);
            }

            return cliente;
        }

        private static Guid ExtrairApiKey(string token)
        {
            JwtSecurityToken jwt = ExtrairJwt(token);

            Guid apikey = new Guid(jwt.Issuer);
            return apikey;
        }

        private static string ExtrairBodyHash(string token)
        {
            JwtSecurityToken jwt = ExtrairJwt(token);

            string hash = jwt.Claims.First(claim => claim.Type == BODY_CLAIM).Value;
            return hash;
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