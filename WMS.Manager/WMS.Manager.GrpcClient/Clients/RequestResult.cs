namespace WMS.Manager.GrpcClient.Clients
{
    public class RequestResult<T>
    {
        public RequestResult(T response, bool isSuccess)
        {
            Response = response;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }

        public T Response { get; }
    }
}