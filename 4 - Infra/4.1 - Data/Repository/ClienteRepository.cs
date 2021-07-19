using System;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.infra.Context;
using arroba.suino.webapi.Interfaces.Repository;

namespace arroba.suino.webapi.infra.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private MySqlContext context;

        public ClienteRepository(MySqlContext context)
        {
            this.context = context;
        }

        public async Task<Cliente> GetByApiKey(Guid apikey)
        {
            return context.Clientes.Find(apikey);
        }
    }
}