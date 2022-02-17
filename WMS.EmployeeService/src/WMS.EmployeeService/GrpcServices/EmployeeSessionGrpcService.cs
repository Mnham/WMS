using Google.Protobuf;
using Grpc.Core;
using MediatR;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate;
using WMS.EmployeeService.Domain.Infrastructure.Commands.EmployeeSessionAggregate.Responses;
using WMS.EmployeeService.Domain.Infrastructure.Helpers;
using WMS.EmployeeService.Grpc;

namespace WMS.EmployeeService.GrpcServices
{
    /// <summary>
    /// Представляет сервис сессий.
    /// </summary>
    public class EmployeeSessionGrpcService : EmployeeSessionApiGrpc.EmployeeSessionApiGrpcBase
    {
        /// <summary>
        /// Экземпляр медиатора.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeSessionGrpcService"/>.
        /// </summary>
        public EmployeeSessionGrpcService(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Добавляет сессию.
        /// </summary>
        public override async Task<EmployeeSessionGrpc> Insert(EmployeeSessionGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                InsertEmployeeSessionQueryResponse response = await _mediator.Send(new InsertEmployeeSessionQuery
                {
                    Session = EmployeeSessionMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return EmployeeSessionMapper.DtoToGrpc(response.Session);
            });

        /// <summary>
        /// Обновляет сессию.
        /// </summary>
        public override async Task<EmployeeSessionGrpc> Update(EmployeeSessionGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                UpdateEmployeeSessionQueryResponse response = await _mediator.Send(new UpdateEmployeeSessionQuery
                {
                    Session = EmployeeSessionMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return EmployeeSessionMapper.DtoToGrpc(response.Session);
            });

        /// <summary>
        /// Возвращает сессиию по идентификатору.
        /// </summary>
        public override async Task<EmployeeSessionGrpc> GetById(IntIdModel request, ServerCallContext context)
        {
            GetByIdResponse response = await _mediator.Send(new GetById
            {
                SessionId = request.Id
            }, context.CancellationToken);

            return EmployeeSessionMapper.DtoToGrpc(response.Session);
        }

        /// <summary>
        /// Обрабатывает исключения.
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