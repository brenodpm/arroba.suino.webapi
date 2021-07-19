using System;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;

namespace arroba.suino.webapi.Interfaces.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente> GetByApiKey(Guid apikey);
    }
}