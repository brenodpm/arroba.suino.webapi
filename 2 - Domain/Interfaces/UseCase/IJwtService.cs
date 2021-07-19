using System.Threading.Tasks;

namespace arroba.suino.webapi.Interfaces.UseCase
{
    public interface IJwtService
    {
        Task ValidarJwtComBody(string token, string body);
    }
}