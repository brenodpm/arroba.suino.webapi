using arroba.suino.webapi.Domain.Entities;

namespace arroba.suino.webapi.Interfaces.Repository
{
    public interface IClienteRepository
    {
        Cliente GetByApiKey(string apikey);
    }
}