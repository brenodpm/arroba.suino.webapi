using arroba.suino.webapi.Domain.Interfaces.UseCase;
using arroba.suino.webapi.infra.Repository;
using arroba.suino.webapi.Interfaces.Repository;
using arroba.suino.webapi.Service.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace arroba.suino.webapi.Application
{
    public static class StartupExt
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUseCaseExample, UseCaseExample>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}