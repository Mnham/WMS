using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WMS.Microservice.Infrastructure.Filters;
using WMS.Microservice.Infrastructure.Interceptors;
using WMS.Microservice.Infrastructure.StartupFilters;
using WMS.NomenclatureService.Domain.Infrastructure.Extensions;
using WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureAggregate;
using WMS.NomenclatureService.GrpcServices;

namespace WMS.NomenclatureService
{
    /// <summary>
    /// Представляет конфигурацию приложения.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Создает экземпляр класса <see cref="Startup"/>.
        /// </summary>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// Конфигурирует способ ответа на запрос.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseRouting()
                .UseGrpcWeb()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<NomenclatureGrpsService>().EnableGrpcWeb();
                    endpoints.MapGrpcService<NomenclatureTypeGrpsService>().EnableGrpcWeb();
                    endpoints.MapControllers();
                });

        /// <summary>
        /// Настраивает и регистрирует сервис при послуплении запроса.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseComponents(Configuration)
                .AddRepositories()
                .AddMediatR(typeof(SearchNomenclatureQueryHandler).Assembly)
                .AddSingleton<IStartupFilter, TerminalStartupFilter>()
                .AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());

            services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
        }
    }
}