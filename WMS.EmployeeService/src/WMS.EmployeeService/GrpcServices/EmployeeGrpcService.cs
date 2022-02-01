using Google.Protobuf;

using Grpc.Core;

using MediatR;
using WMS.ClassLibrary.Extensions;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.GrpcServices
{
    /// <summary>
    /// Представляет сервис данных сотрудников.
    /// </summary>
    public class EmployeeGrpcService : EmployeeApiGrpc.EmployeeApiGrpcBase
    {
        /// <summary>
        /// Предоставляет экземпляр медиатора.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeGrpcService"/>.
        /// </summary>
        public EmployeeGrpcService(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Добовляет данные о сотрудника.
        /// </summary>
        public override async Task<EmployeeGrpc> Insert(EmployeeGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                InsertEmployeeQueryResponse response = await _mediator.Send(new InsertEmployeeQuery()
                {
                    Employee = EmployeeMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return EmployeeMapper.DtoToGrpc(response.Employee);
            });

        /// <summary>
        /// Выполняет поиск данных сотрудника.
        /// </summary>
        public override async Task<EmployeeList> Search(EmployeeSearchFilter request, ServerCallContext context)
        {
            SearchEmployeeQueryResponse response = await _mediator.Send(new SearchEmployeeQuery
            {
                EmployeeId = request.EmployeeId,
                EmployeeName = request.EmployeeName
            }, context.CancellationToken);

            return new EmployeeList
            {
                Employees = { response.Items.Map(EmployeeMapper.DtoToGrpc) }
            };
        }

        /// <summary>
        /// Выполняет обновление данных сотрудника.
        /// </summary>
        public override async Task<EmployeeGrpc> Update(EmployeeGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                UpdateEmployeeQueryResponse response = await _mediator.Send(new UpdateEmployeeQuery
                {
                    Employee = EmployeeMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return EmployeeMapper.DtoToGrpc(response.Employee);
            });

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