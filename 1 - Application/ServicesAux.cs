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
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}