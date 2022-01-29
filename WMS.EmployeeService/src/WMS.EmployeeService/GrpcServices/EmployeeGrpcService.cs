using Google.Protobuf;

using Grpc.Core;

using MediatR;

using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.GrpcServices
{
    public class EmployeeGrpcService : EmployeeApiGrpc.EmployeeApiGrpcBase
    {

        /// <summary>
        /// Экземпляр медиатора.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Создает экземпляр класса <see cref="EmployeeGrpcService"/>.
        /// </summary>
        public EmployeeGrpcService(IMediator mediator) => _mediator = mediator;

        public override async Task<EmployeeGrpc> Insert(EmployeeGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                InsertEmployeeQueryResponse response = await _mediator.Send(new InsertEmployeeQuery()
                {
                    Employee = EmployeeMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return EmployeeMapper.DtoToGrpc(response.Employee);
            });

        public override async Task<EmployeeList> Search(EmployeeSearchFilter request, ServerCallContext context) =>
     throw new Exception();

        public override async Task<EmployeeGrpc> Update(EmployeeGrpc request, ServerCallContext context) =>
         throw new Exception();

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
