namespace arroba.suino.webapi.Interfaces.UseCase
{
    public interface IJwtService
    {
        void ValidarJwtEBody(string header, string body);
    }
}