using System.Threading.Tasks;

namespace arroba.suino.webapi.Interfaces.UseCase
{
    public interface IAccessTokenService
    {
        Task Validar(string accessToken, string claimExigida);
    }
}