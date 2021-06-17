using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.Domain.Interfaces.UseCase;
using arroba.suino.webapi.infra.Context;
using arroba.suino.webapi.Interfaces.Repository;

namespace arroba.suino.webapi.Service.UseCases
{
    public class UseCaseExample : IUseCaseExample
    {
        private readonly IUsuarioRepository repository;
        public UseCaseExample(IUsuarioRepository repository)
        {
            this.repository = repository;
        }
        async Task<IList<Usuario>> IUseCaseExample.GetUsuarios()
        {
            IList<Usuario> usuario = await repository.Select();
            return usuario;
        }
    }
}