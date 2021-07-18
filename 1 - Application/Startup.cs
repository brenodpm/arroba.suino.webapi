using arroba.suino.webapi.Application.Filters;
using arroba.suino.webapi.Application.Map;
using arroba.suino.webapi.Domain.Interfaces.UseCase;
using arroba.suino.webapi.infra.Context;
using arroba.suino.webapi.infra.Repository;
using arroba.suino.webapi.Interfaces.Repository;
using arroba.suino.webapi.Service.UseCases;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace arroba.suino.webapi.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string dbConnectionString = Configuration.GetConnectionString("DotNetCoreMySQLAppConnection");
            services.AddDbContext<MySqlContext>(opt => opt.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapping()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddServices();

            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "arroba.suino.webapi.Application", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "arroba.suino.webapi.Application v1"));
           
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
