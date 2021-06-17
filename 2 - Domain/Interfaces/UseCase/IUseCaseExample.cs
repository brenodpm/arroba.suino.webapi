using System.Collections.Generic;
using System.Threading.Tasks;
using arroba.suino.webapi.Domain.Entities;

namespace arroba.suino.webapi.Domain.Interfaces.UseCase
{
    public interface IUseCaseExample
    {
        Task<IList<Usuario>> GetUsuarios();
    }
}