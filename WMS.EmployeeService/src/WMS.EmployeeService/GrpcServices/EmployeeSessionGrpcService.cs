using Google.Protobuf;
using Grpc.Core;
using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.GrpcServices
{
    public class EmployeeSessionGrpcService : EmployeeSessionApiGrpc.EmployeeSessionApiGrpcBase
    {
        private readonly IMediator _mediator;

        public EmployeeSessionGrpcService(IMediator mediator) => _mediator = mediator;

        public override async Task<EmployeeSessionGrpc> Insert(EmployeeSessionGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                InsertEmployeeSessionQueryResponse response = await _mediator.Send(new InsertEmployeeSessionQuery
                {
                    Session = EmployeeSessionMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return EmployeeSessionMapper.DtoToGrpc(response.Session);
            });

        public override async Task<EmployeeSessionGrpc> Update(EmployeeSessionGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                UpdateEmployeeSessionQueryResponse response = await _mediator.Send(new UpdateEmployeeSessionQuery
                {
                    Session = EmployeeSessionMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return EmployeeSessionMapper.DtoToGrpc(response.Session);
            });

        public override async Task<EmployeeSessionGrpc> GetById(IntIdModel request, ServerCallContext context)
        {
            SearchEmployeeSessionQueryResponse response = await _mediator.Send(new SearchEmployeeSessionQuery
            {
                SessionId = request.Id
            }, context.CancellationToken);

            return EmployeeSessionMapper.DtoToGrpc(response.Session);
        }

        /// <summary>
        /// Обрабатывает исключение.
        /// </summary>
        private static async Task<T> HandleException<T>(Func<Task<T>> func) where T : IMessage<T>
        {
            try
            {
                return await func();
            }
            catch (ArgumentException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }
    }
}