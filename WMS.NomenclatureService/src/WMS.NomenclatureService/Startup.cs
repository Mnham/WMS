using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WMS.ClassLibrary.Infrastructure.Filters;
using WMS.ClassLibrary.Infrastructure.Interceptors;
using WMS.ClassLibrary.Infrastructure.StartupFilters;
using WMS.NomenclatureService.Domain.Infrastructure.Extensions;
using WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureAggregate;
using WMS.NomenclatureService.GrpcServices;

namespace WMS.NomenclatureService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseRouting()
                .UseGrpcWeb()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<NomenclatureGrpsService>().EnableGrpcWeb();
                    endpoints.MapGrpcService<NomenclatureTypeGrpsService>().EnableGrpcWeb();
                    endpoints.MapControllers();
                });

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