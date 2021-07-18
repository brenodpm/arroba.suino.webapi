using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Xunit;
using Moq;

namespace arroba.suino.webapi.test.Services
{
    public class JwtServiceTest
    {
        private readonly Mock<IClienteRepository> clienteMock = new Mock<IClienteRepository>();

        [Fact]
        public void TJWT_Valido()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid().ToString(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid().ToString(),
                Ativo = true,
                CodDesenvolvedor = Guid.NewGuid().ToString()
            };
            string apikey = cliente.ApiKey;
            string secret = cliente.ApiSecret;
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            jwtService.ValidarJwtEBody(header, body);
        }

        [Fact]
        public void TJWT_ClienteInexistente()
        {
            string apikey = Guid.NewGuid().ToString();
            string secret = cliente.ApiSecret;
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns<Cliente>(null);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtEBody(header, body));

            Assert.Equal("Terminal de acesso inválido", atual.message);
        }

        [Fact]
        public void TJWT_ClienteDesativado()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid().ToString(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid().ToString(),
                Ativo = false,
                CodDesenvolvedor = Guid.NewGuid().ToString()
            };
            string apikey = cliente.ApiKey;
            string secret = cliente.ApiSecret;
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtEBody(header, body));

            Assert.Equal("Terminal de acesso inválido", atual.message);
        }

        [Fact]
        public void TJWT_AssinaturaInvalida()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid().ToString(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid().ToString(),
                Ativo = true,
                CodDesenvolvedor = Guid.NewGuid().ToString()
            };
            string apikey = cliente.ApiKey;
            string secret = Guid.NewGuid().ToString();
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtEBody(header, body));

            Assert.Equal("Terminal de acesso inválido", atual.message);
        }

        [Fact]
        public void TJWT_BodyInvalido()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid().ToString(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid().ToString(),
                Ativo = true,
                CodDesenvolvedor = Guid.NewGuid().ToString()
            };
            string apikey = cliente.ApiKey;
            string secret = cliente.ApiSecret;
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, "{\"deu\":\"errado\"}");

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtEBody(header, body));

            Assert.Equal("O corpo da mensagem não condiz com o cabeçalho informado", atual.message);
        }

        public static string GenerateToken(string apikey, string secret, string accessToken, string body)
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
            return tokenHandler.WriteToken(token);
        }

    }
}
