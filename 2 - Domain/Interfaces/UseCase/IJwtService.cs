namespace arroba.suino.webapi.Interfaces.UseCase
{
    public interface IJwtService
    {
        void ValidarJwtComBody(string token, string body);
    }
}