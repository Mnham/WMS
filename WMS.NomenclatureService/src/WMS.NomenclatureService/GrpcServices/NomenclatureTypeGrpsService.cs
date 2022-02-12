using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using MediatR;

using System;
using System.Threading.Tasks;

using WMS.Microservice.Extensions;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.GrpcServices
{
    /// <summary>
    /// Представляет сервис типа номенклатуры.
    /// </summary>
    public class NomenclatureTypeGrpsService : NomenclatureTypeGrpcService.NomenclatureTypeGrpcServiceBase
    {
        /// <summary>
        /// Экземпляр медиатора.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureTypeGrpsService"/>.
        /// </summary>
        public NomenclatureTypeGrpsService(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Возвращает все типы номенклатур.
        /// </summary>
        public override async Task<NomenclatureTypeList> GetAll(Empty request, ServerCallContext context)
        {
            GetAllNomenclatureTypeQueryResponse response = await _mediator.Send(new GetAllNomenclatureTypeQuery(), context.CancellationToken);

            return new NomenclatureTypeList
            {
                NomenclatureTypes = { response.Items.Map(NomenclatureTypeMapper.DtoToGrpc) }
            };
        }

        /// <summary>
        /// Добавляет тип номенклатуры.
        /// </summary>
        public override async Task<NomenclatureTypeGrpc> Insert(NomenclatureTypeGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                InsertNomenclatureTypeQueryResponse response = await _mediator.Send(new InsertNomenclatureTypeQuery()
                {
                    NomenclatureType = NomenclatureTypeMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return NomenclatureTypeMapper.DtoToGrpc(response.NomenclatureType);
            });

        /// <summary>
        /// Обновляет тип номенклатуры.
        /// </summary>
        public override async Task<NomenclatureTypeGrpc> Update(NomenclatureTypeGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                UpdateNomenclatureTypeQueryResponse response = await _mediator.Send(new UpdateNomenclatureTypeQuery()
                {
                    NomenclatureType = NomenclatureTypeMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return NomenclatureTypeMapper.DtoToGrpc(response.NomenclatureType);
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