using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Xunit;
using Moq;
using arroba.suino.webapi.Interfaces.Repository;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.Interfaces.UseCase;
using arroba.suino.webapi.Service.UseCases;
using arroba.suino.webapi.Domain.Exceptions;

namespace arroba.suino.webapi.test.Service
{
    public class JwtServiceTest
    {
        private readonly Mock<IClienteRepository> clienteMock = new Mock<IClienteRepository>();

        [Fact]
        public void TJWT_Valido()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid(),
                Ativo = true,
                CodDesenvolvedor = Guid.NewGuid()
            };
            Guid apikey = cliente.ApiKey;
            Guid secret = cliente.ApiSecret;
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            jwtService.ValidarJwtComBody(header, body);
        }

        [Fact]
        public void TJWT_ClienteInexistente()
        {
            Guid apikey = Guid.NewGuid();
            Guid secret = Guid.NewGuid();
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns<Cliente>(null);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtComBody(header, body));

            Assert.Equal("Terminal de acesso inválido", atual.Message);
        }

        [Fact]
        public void TJWT_ClienteDesativado()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid(),
                Ativo = false,
                CodDesenvolvedor = Guid.NewGuid()
            };
            Guid apikey = cliente.ApiKey;
            Guid secret = cliente.ApiSecret;
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtComBody(header, body));

            Assert.Equal("Terminal de acesso inválido", atual.Message);
        }

        [Fact]
        public void TJWT_AssinaturaInvalida()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid(),
                Ativo = true,
                CodDesenvolvedor = Guid.NewGuid()
            };
            Guid apikey = cliente.ApiKey;
            Guid secret = Guid.NewGuid();
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, body);

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtComBody(header, body));

            Assert.Equal("Terminal de acesso inválido", atual.Message);
        }

        [Fact]
        public void TJWT_BodyInvalido()
        {
            Cliente cliente = new Cliente
            {
                ApiKey = Guid.NewGuid(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid(),
                Ativo = true,
                CodDesenvolvedor = Guid.NewGuid()
            };
            Guid apikey = cliente.ApiKey;
            Guid secret = cliente.ApiSecret;
            string accessToken = Guid.NewGuid().ToString();
            string body = "{\"deu\":\"certo\"}";

            clienteMock.Setup(c => c.GetByApiKey(apikey)).Returns(cliente);

            string header = GenerateToken(apikey, secret, accessToken, "{\"deu\":\"errado\"}");

            IJwtService jwtService = new JwtService(clienteMock.Object);

            JwtServiceException atual = Assert.Throws<JwtServiceException>(() => jwtService.ValidarJwtComBody(header, body));

            Assert.Equal("O corpo da mensagem não condiz com o cabeçalho informado", atual.Message);
        }

        public static string GenerateToken(Guid apikey, Guid secret, string accessToken, string body)
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



            var key = Encoding.ASCII.GetBytes(secret.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = apikey.ToString(),
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
