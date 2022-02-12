using Google.Protobuf;

using Grpc.Core;

using MediatR;

using System;
using System.Threading.Tasks;

using WMS.Microservice.Extensions;
using WMS.NomenclatureService.Domain.Exceptions;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.GrpcServices
{
    /// <summary>
    /// Представляет сервис номенклатуры.
    /// </summary>
    public class NomenclatureGrpsService : NomenclatureGrpcService.NomenclatureGrpcServiceBase
    {
        /// <summary>
        /// Экземпляр медиатора.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Создает экземпляр класса <see cref="NomenclatureGrpsService"/>.
        /// </summary>
        public NomenclatureGrpsService(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Добавляет номенклатуру.
        /// </summary>
        public override async Task<NomenclatureGrpc> Insert(NomenclatureGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                InsertNomenclatureQueryResponse response = await _mediator.Send(new InsertNomenclatureQuery()
                {
                    Nomenclature = NomenclatureMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return NomenclatureMapper.DtoToGrpc(response.Nomenclature);
            });

        /// <summary>
        /// Выполняет поиск.
        /// </summary>
        public override async Task<NomenclatureList> Search(NomenclatureSearchFilter request, ServerCallContext context)
        {
            SearchNomenclatureQueryResponse response = await _mediator.Send(new SearchNomenclatureQuery()
            {
                NomenclatureId = request.NomenclatureId,
                NomenclatureName = request.NomenclatureName,
                NomenclatureTypeId = request.NomenclatureTypeId
            }, context.CancellationToken);

            return new NomenclatureList
            {
                Nomenclatures = { response.Items.Map(NomenclatureMapper.DtoToGrpc) }
            };
        }

        /// <summary>
        /// Обновляет номенклатуру.
        /// </summary>
        public override async Task<NomenclatureGrpc> Update(NomenclatureGrpc request, ServerCallContext context) =>
            await HandleException(async () =>
            {
                UpdateNomenclatureQueryResponse response = await _mediator.Send(new UpdateNomenclatureQuery()
                {
                    Nomenclature = NomenclatureMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return NomenclatureMapper.DtoToGrpc(response.Nomenclature);
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
            catch (NegativeOrZeroValueException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
        }
    }
}