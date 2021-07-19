using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.Domain.Exceptions;
using arroba.suino.webapi.Interfaces.Repository;
using arroba.suino.webapi.Interfaces.UseCase;
using arroba.suino.webapi.Service.UseCase;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace arroba.suino.webapi.test.Service
{
    public class AccessTokenServiceTest
    {
        private readonly Mock<ISessaoRepository> sessaoRepository = new Mock<ISessaoRepository>();
        private readonly Mock<IEmpresaRepository> empresaRepository = new Mock<IEmpresaRepository>();
        private readonly Mock<IGrupoRepository> grupoRepository = new Mock<IGrupoRepository>();


        [Fact]
        public async Task AccessToken_Valido()
        {
            String claimExigida = "claim";
            Empresa empresa = new Empresa
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                NomeEmpresa = "Nome da empresa",
                Security = Guid.NewGuid(),
                Email = "email@dominio.com",
                EmailValidado = true,
                DataCadastro = DateTime.Now
            };

            Grupo grupo = new Grupo
            {
                Id = Guid.NewGuid(),
                CodEmpresa = empresa.Id,
                NomeGrupo = "Nome grupo",
                Ativo = true,
                Permissoes = new List<string> { claimExigida }
            };

            Sessao sessao = new Sessao
            {
                Id = Guid.NewGuid(),
                CodEmpresa = empresa.Id,
                CodUsuario = Guid.NewGuid(),
                CodGrupo = grupo.Id,
                ApiKey = Guid.NewGuid(),
                Ativo = true,
                PrimeiroAcesso = DateTime.Now,
                UltimoAcesso = DateTime.Now,
                Descricao = "",
                Localizacao = ""
            };

            string accessToken = GenerateToken(sessao.Id, empresa.Security);

            sessaoRepository.Setup(c => c.Select(sessao.Id)).ReturnsAsync(sessao);
            empresaRepository.Setup(c => c.Select(empresa.Id)).ReturnsAsync(empresa);
            grupoRepository.Setup(c => c.Select(grupo.Id)).ReturnsAsync(grupo);

            IAccessTokenService service = new AccessTokenService(sessaoRepository.Object, empresaRepository.Object, grupoRepository.Object);
            await service.Validar(accessToken, claimExigida);
        }

        [Fact]
        public async Task SessaoInesistente()
        {
            String claimExigida = "claim";
            Guid sessao = default;
            Guid security = default;

            string accessToken = GenerateToken(sessao, security);

            sessaoRepository.Setup(c => c.Select(sessao)).ReturnsAsync((Sessao)null);

            IAccessTokenService service = new AccessTokenService(sessaoRepository.Object, empresaRepository.Object, grupoRepository.Object);

            AccessTokenServiceException atual = await Assert.ThrowsAsync<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

            Assert.Equal("Sessão expirada", atual.Message);
        }

        [Fact]
        public async Task SessaoDesativada()
        {
            String claimExigida = "claim";
            Sessao sessao = new Sessao
            {
                Id = Guid.NewGuid(),
                CodEmpresa = Guid.NewGuid(),
                CodUsuario = Guid.NewGuid(),
                CodGrupo = Guid.NewGuid(),
                ApiKey = Guid.NewGuid(),
                Ativo = false,
                PrimeiroAcesso = DateTime.Now,
                UltimoAcesso = DateTime.Now,
                Descricao = "",
                Localizacao = ""
            };

            string accessToken = GenerateToken(sessao.Id, Guid.NewGuid());

            sessaoRepository.Setup(c => c.Select(sessao.Id)).ReturnsAsync(sessao);

            IAccessTokenService service = new AccessTokenService(sessaoRepository.Object, empresaRepository.Object, grupoRepository.Object);

            AccessTokenServiceException atual = await Assert.ThrowsAsync<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

            Assert.Equal("Sessão expirada", atual.Message);
        }

        [Fact]
        public async Task AssinaturaInvalida()
        {
            String claimExigida = "claim";
            Empresa empresa = new Empresa
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                NomeEmpresa = "Nome da empresa",
                Security = Guid.NewGuid(),
                Email = "email@dominio.com",
                EmailValidado = true,
                DataCadastro = DateTime.Now
            };

            Sessao sessao = new Sessao
            {
                Id = Guid.NewGuid(),
                CodEmpresa = empresa.Id,
                CodUsuario = Guid.NewGuid(),
                CodGrupo = Guid.NewGuid(),
                ApiKey = Guid.NewGuid(),
                Ativo = true,
                PrimeiroAcesso = DateTime.Now,
                UltimoAcesso = DateTime.Now,
                Descricao = "",
                Localizacao = ""
            };

            string accessToken = GenerateToken(sessao.Id, Guid.NewGuid());

            sessaoRepository.Setup(c => c.Select(sessao.Id)).ReturnsAsync(sessao);
            empresaRepository.Setup(c => c.Select(empresa.Id)).ReturnsAsync(empresa);

            IAccessTokenService service = new AccessTokenService(sessaoRepository.Object, empresaRepository.Object, grupoRepository.Object);

            AccessTokenServiceException atual = await Assert.ThrowsAsync<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

            Assert.Equal("Sessão expirada", atual.Message);
        }

        [Fact]
        public async Task ClaimNaoEncontrada()
        {
            String claimExigida = "claim";
            Empresa empresa = new Empresa
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                NomeEmpresa = "Nome da empresa",
                Security = Guid.NewGuid(),
                Email = "email@dominio.com",
                EmailValidado = true,
                DataCadastro = DateTime.Now
            };

            Grupo grupo = new Grupo
            {
                Id = Guid.NewGuid(),
                CodEmpresa = empresa.Id,
                NomeGrupo = "Nome grupo",
                Ativo = true,
                Permissoes = new List<string>()
            };

            Sessao sessao = new Sessao
            {
                Id = Guid.NewGuid(),
                CodEmpresa = empresa.Id,
                CodUsuario = Guid.NewGuid(),
                CodGrupo = grupo.Id,
                ApiKey = Guid.NewGuid(),
                Ativo = true,
                PrimeiroAcesso = DateTime.Now,
                UltimoAcesso = DateTime.Now,
                Descricao = "",
                Localizacao = ""
            };

            string accessToken = GenerateToken(sessao.Id, empresa.Security);

            sessaoRepository.Setup(c => c.Select(sessao.Id)).ReturnsAsync(sessao);
            empresaRepository.Setup(c => c.Select(empresa.Id)).ReturnsAsync(empresa);
            grupoRepository.Setup(c => c.Select(grupo.Id)).ReturnsAsync(grupo);

            IAccessTokenService service = new AccessTokenService(sessaoRepository.Object, empresaRepository.Object, grupoRepository.Object);

            AccessTokenServiceException atual = await Assert.ThrowsAsync<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

            Assert.Equal("", atual.Message);
        }


        public static string GenerateToken(Guid codSessao, Guid security)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(security.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = codSessao.ToString(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
