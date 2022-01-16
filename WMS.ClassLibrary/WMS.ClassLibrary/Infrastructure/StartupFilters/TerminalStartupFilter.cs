using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using System;

using WMS.ClassLibrary.Infrastructure.Middlewares;

namespace WMS.ClassLibrary.Infrastructure.StartupFilters
{
    public class TerminalStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) =>
            app =>
            {
                app.Map("/ready", builder => builder.UseMiddleware<ReadyMiddleware>());
                next(app);
            };
    }
}