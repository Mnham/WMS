namespace WMS.Manager.GrpcClient.Clients
{
    /// <summary>
    /// Представляет результат запроса.
    /// </summary>
    public class RequestResult<T>
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="RequestResult"/>.
        /// </summary>
        public RequestResult(T response, bool isSuccess)
        {
            Response = response;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Статус ответа.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Ответ.
        /// </summary>
        public T Response { get; }
    }
}