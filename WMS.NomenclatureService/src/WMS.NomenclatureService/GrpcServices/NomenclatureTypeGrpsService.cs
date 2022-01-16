using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using MediatR;

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

        public override Task<NomenclatureTypeGrpc> Insert(NomenclatureTypeGrpc request, ServerCallContext context) => base.Insert(request, context);

        public override Task<NomenclatureTypeGrpc> Update(NomenclatureTypeGrpc request, ServerCallContext context) => base.Update(request, context);
    }
}