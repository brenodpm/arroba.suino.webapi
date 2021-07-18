using arroba.suino.webapi.Interfaces.Repository;
using arroba.suino.webapi.Interfaces.UseCase;

namespace arroba.suino.webapi.Service.UseCases
{
    public class JwtService : IJwtService
    {
        private readonly IClienteRepository clienteRepository;

        public JwtService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public void ValidarJwtEBody(string header, string body)
        {
            throw new System.NotImplementedException();
        }
    }
}