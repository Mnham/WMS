using Google.Protobuf;

using Grpc.Net.Client;
using Grpc.Net.Client.Web;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WMS.GrpcClient.Clients
{
    /// <summary>
    /// Представляет базовый класс Grpc-клиента.
    /// </summary>
    public abstract class GrpcClientBase
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="GrpcClientBase"/>.
        /// </summary>
        protected GrpcClientBase(string address) => Channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
        {
            HttpHandler = new GrpcWebHandler(new HttpClientHandler())
        });

        /// <summary>
        /// Канал подключения.
        /// </summary>
        protected GrpcChannel Channel { get; }

        /// <summary>
        /// Обработчик исключения.
        /// </summary>
        private Action<Exception> _exceptionHandler;

        /// <summary>
        /// Возвращает результат запроса.
        /// </summary>
        protected async Task<RequestResult<T>> Execute<T>(Func<Task<T>> func) where T : IMessage<T>
        {
            try
            {
                return new RequestResult<T>(await func(), true);
            }
            catch (Exception ex)
            {
                _exceptionHandler?.Invoke(ex);
                return new RequestResult<T>(default, false);
            }
        }

        /// <summary>
        /// Устанавливает обработчик исключения.
        /// </summary>
        public void SetExceptionHandler(Action<Exception> exceptionHandler) => _exceptionHandler = exceptionHandler;
    }
}