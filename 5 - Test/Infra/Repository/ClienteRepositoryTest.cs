using System;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.infra.Context;
using arroba.suino.webapi.infra.Repository;
using arroba.suino.webapi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace arroba.suino.webapi.test.Repository
{
    public class ClienteRepositoryTest
    {
        [Fact]
        public void GetByApiKey()
        {
            Cliente esperado = new Cliente
            {
                ApiKey = Guid.NewGuid(),
                Nome = "Cliente",
                ApiSecret = Guid.NewGuid(),
                Ativo = true,
                CodDesenvolvedor = Guid.NewGuid()
            };


            var options = new DbContextOptionsBuilder<MySqlContext>()
                        .UseInMemoryDatabase(databaseName: "MySqlContext")
                        .Options;

            using (var context = new MySqlContext(options))
            {
                context.Clientes.Add(esperado);
                context.SaveChanges();
            }


            // Use a clean instance of the context to run the test
            using (var context = new MySqlContext(options))
            {

                IClienteRepository repository = new ClienteRepository(context);
                Cliente atual = repository.GetByApiKey(esperado.ApiKey);

                Assert.NotNull(atual);
                Assert.Equal(esperado.ApiKey, atual.ApiKey);
                Assert.Equal(esperado.ApiSecret, atual.ApiSecret);
                Assert.Equal(esperado.Ativo, atual.Ativo);
                Assert.Equal(esperado.CodDesenvolvedor, atual.CodDesenvolvedor);
                Assert.Equal(esperado.Nome, atual.Nome);
            }
        }


        [Fact]
        public void GetByApiKey_NaoEncontrado()
        {
            var options = new DbContextOptionsBuilder<MySqlContext>()
                        .UseInMemoryDatabase(databaseName: "MySqlContext")
                        .Options;


            // Use a clean instance of the context to run the test
            using (var context = new MySqlContext(options))
            {

                IClienteRepository repository = new ClienteRepository(context);
                Cliente atual = repository.GetByApiKey(Guid.NewGuid());

                Assert.Null(atual);
            }
        }
    }
}