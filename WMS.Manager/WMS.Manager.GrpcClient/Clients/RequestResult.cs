namespace WMS.Manager.GrpcClient.Clients
{
    public class RequestResult<T>
    {
        public bool IsSuccess { get; }

        public T Response { get; }

        public RequestResult(T response, bool isSuccess)
        {
            Response = response;
            IsSuccess = isSuccess;
        }
    }
}