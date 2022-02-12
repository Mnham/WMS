using Microsoft.AspNetCore.Http;

using System.Net;
using System.Threading.Tasks;

namespace WMS.Microservice.Infrastructure.Middlewares
{
    /// <summary>
    /// Представляет обработчик запроса готовности микросервиса.
    /// </summary>
    public class ReadyMiddleware
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="ReadyMiddleware"/>.
        /// </summary>
        public ReadyMiddleware(RequestDelegate next)
        {
        }

        /// <summary>
        /// Выполняет обработку запроса.
        /// </summary>
        public async Task InvokeAsync(HttpContext context) =>
            context.Response.StatusCode = (int)HttpStatusCode.OK;
    }
}