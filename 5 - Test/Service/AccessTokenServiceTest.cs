using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace arroba.suino.webapi.test.Service
{
    public class AccessTokenServiceTest
    {
        private readonly Mock<ISessaoRepository> sessaoRepository = new Mock<ISessaoRepository>();
        private readonly Mock<IEmpresaRepository> empresaRepository = new Mock<IEmpresaRepository>();
        private readonly Mock<IGrupoRepository> empresaRepository = new Mock<IGrupoRepository>();


        [Fact]
        public void AccessToken_Valido()
        {
            String claimExigida = "claim";
            Empresa empresa = new Empresa
            {
                CodEmpresa = Guid.NewGuid(),
                Ativo = true,
                NomeEmpresa = "Nome da empresa",
                Security = Guid.NewGuid(),
                Email = "email@dominio.com",
                EmailValidado = true,
                DataCadastro = DateTime.Now
            };

            Grupo grupo = new Grupo
            {
                CodGrupo = Guid.NewGuid(),
                CodEmpresa = empresa.CodEmpresa,
                NomeGrupo = "Nome grupo",
                Ativo = true,
                Permissoes = $"[\"{ claimExigida }\"]"
            };

            Sessao sessao = new Sessao
            {
                CodSessao = Guid.NewGuid(),
                CodEmpresa = empresa.CodEmpresa,
                CodUsuario = Guid.NewGuid(),
                CodGrupo = grupo.CodGrupo,
                ApiKey = Guid.NewGuid(),
                Ativo = true,
                PrimeiroAcesso = DateTime.Now,
                UltimoAcesso = DateTime.Now,
                Descricao = "",
                Localizacao = ""
            };

            string accessToken = GenerateToken(sessao.CodSessao, empresa.Security);

            sessaoRepository.Setup(c => c.GetById(sessao.CodSessao)).Returns(sessao);
            empresaRepository.Setup(c => c.GetById(empresa.CodEmpresa)).Returns(empresa);
            grupoRepository.Setup(c => c.GetById(grupo.CodGrupo)).Returns(grupo);

            IAccessTokenServiceTest service = new AccessTokenServiceTest();
            service.Validar(accessToken, claimExigida);
        }

        [Fact]
        public void SessaoInesistente()
        {
            String claimExigida = "claim";
            Guid sessao = default;
            Guid security = default;

            string accessToken = GenerateToken(sessao, security);

            sessaoRepository.Setup(c => c.GetById(sessao)).Returns<Sessao>(null);

            IAccessTokenServiceTest service = new AccessTokenServiceTest();
            
            AccessTokenServiceException atual = Assert.Throws<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

            Assert.Equal("Sessão expirada", atual.Message);
        }

        [Fact]
        public void SessaoDesativada()
        {
            String claimExigida = "claim";
            Sessao sessao = new Sessao
            {
                CodSessao = Guid.NewGuid(),
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

            string accessToken = GenerateToken(sessao.CodSessao, Guid.NewGuid());

            sessaoRepository.Setup(c => c.GetById(sessao.CodSessao)).Returns(sessao);

            IAccessTokenServiceTest service = new AccessTokenServiceTest();
            
            AccessTokenServiceException atual = Assert.Throws<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

            Assert.Equal("Sessão expirada", atual.Message);
        }

        [Fact]
        public void AssinaturaInvalida()
        {
            String claimExigida = "claim";
            Empresa empresa = new Empresa
            {
                CodEmpresa = Guid.NewGuid(),
                Ativo = true,
                NomeEmpresa = "Nome da empresa",
                Security = Guid.NewGuid(),
                Email = "email@dominio.com",
                EmailValidado = true,
                DataCadastro = DateTime.Now
            };

            Sessao sessao = new Sessao
            {
                CodSessao = Guid.NewGuid(),
                CodEmpresa = empresa.CodEmpresa,
                CodUsuario = Guid.NewGuid(),
                CodGrupo = grupo.CodGrupo,
                ApiKey = Guid.NewGuid(),
                Ativo = true,
                PrimeiroAcesso = DateTime.Now,
                UltimoAcesso = DateTime.Now,
                Descricao = "",
                Localizacao = ""
            };

            string accessToken = GenerateToken(sessao.CodSessao, Guid.NewGuid());

            sessaoRepository.Setup(c => c.GetById(sessao.CodSessao)).Returns(sessao);
            empresaRepository.Setup(c => c.GetById(empresa.CodEmpresa)).Returns(empresa);
            grupoRepository.Setup(c => c.GetById(grupo.CodGrupo)).Returns(grupo);

            IAccessTokenServiceTest service = new AccessTokenServiceTest();
            
            AccessTokenServiceException atual = Assert.Throws<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

            Assert.Equal("Sessão expirada", atual.Message);
        }

        [Fact]
        public void ClaimNaoEncontrada()
        {
            String claimExigida = "claim";
            Empresa empresa = new Empresa
            {
                CodEmpresa = Guid.NewGuid(),
                Ativo = true,
                NomeEmpresa = "Nome da empresa",
                Security = Guid.NewGuid(),
                Email = "email@dominio.com",
                EmailValidado = true,
                DataCadastro = DateTime.Now
            };

            Grupo grupo = new Grupo
            {
                CodGrupo = Guid.NewGuid(),
                CodEmpresa = empresa.CodEmpresa,
                NomeGrupo = "Nome grupo",
                Ativo = true,
                Permissoes = "[]"
            };

            Sessao sessao = new Sessao
            {
                CodSessao = Guid.NewGuid(),
                CodEmpresa = empresa.CodEmpresa,
                CodUsuario = Guid.NewGuid(),
                CodGrupo = grupo.CodGrupo,
                ApiKey = Guid.NewGuid(),
                Ativo = true,
                PrimeiroAcesso = DateTime.Now,
                UltimoAcesso = DateTime.Now,
                Descricao = "",
                Localizacao = ""
            };

            string accessToken = GenerateToken(sessao.CodSessao, empresa.Security);

            sessaoRepository.Setup(c => c.GetById(sessao.CodSessao)).Returns(sessao);
            empresaRepository.Setup(c => c.GetById(empresa.CodEmpresa)).Returns(empresa);
            grupoRepository.Setup(c => c.GetById(grupo.CodGrupo)).Returns(grupo);

            IAccessTokenServiceTest service = new AccessTokenServiceTest();
            
            AccessTokenServiceException atual = Assert.Throws<AccessTokenServiceException>(() => service.Validar(accessToken, claimExigida));

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
