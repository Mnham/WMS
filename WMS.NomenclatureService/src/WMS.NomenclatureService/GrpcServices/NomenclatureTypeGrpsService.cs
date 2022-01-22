using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using MediatR;

using System;
using System.Threading.Tasks;

using WMS.ClassLibrary.Extensions;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.GrpcServices
{
    public class NomenclatureTypeGrpsService : NomenclatureTypeGrpcService.NomenclatureTypeGrpcServiceBase
    {
        private readonly IMediator _mediator;

        public NomenclatureTypeGrpsService(IMediator mediator) => _mediator = mediator;

        public override async Task<NomenclatureTypeList> GetAll(Empty request, ServerCallContext context)
        {
            GetAllNomenclatureTypeQueryResponse response = await _mediator.Send(new GetAllNomenclatureTypeQuery(), context.CancellationToken);

            return new NomenclatureTypeList
            {
                NomenclatureTypes = { response.Items.Map(NomenclatureTypeMapper.DtoToGrpc) }
            };
        }

        public override async Task<NomenclatureTypeGrpc> Insert(NomenclatureTypeGrpc request, ServerCallContext context) =>
            await Execute(async () =>
            {
                InsertNomenclatureTypeQueryResponse response = await _mediator.Send(new InsertNomenclatureTypeQuery()
                {
                    NomenclatureType = NomenclatureTypeMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return NomenclatureTypeMapper.DtoToGrpc(response.NomenclatureType);
            });

        public override async Task<NomenclatureTypeGrpc> Update(NomenclatureTypeGrpc request, ServerCallContext context) =>
            await Execute(async () =>
            {
                UpdateNomenclatureTypeQueryResponse response = await _mediator.Send(new UpdateNomenclatureTypeQuery()
                {
                    NomenclatureType = NomenclatureTypeMapper.GrpcToDto(request)
                }, context.CancellationToken);

                return NomenclatureTypeMapper.DtoToGrpc(response.NomenclatureType);
            });

        private static async Task<T> Execute<T>(Func<Task<T>> func) where T : IMessage<T>
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