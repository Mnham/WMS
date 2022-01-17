﻿using Grpc.Core;

using MediatR;

using System.Threading.Tasks;

using WMS.ClassLibrary.Extensions;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.GrpcServices
{
    public class NomenclatureGrpsService : NomenclatureGrpcService.NomenclatureGrpcServiceBase
    {
        private readonly IMediator _mediator;

        public NomenclatureGrpsService(IMediator mediator) => _mediator = mediator;

        public override async Task<NomenclatureGrpc> Insert(NomenclatureGrpc request, ServerCallContext context)
        {
            InsertNomenclatureQueryResponse response = await _mediator.Send(new InsertNomenclatureQuery()
            {
                Nomenclature = NomenclatureMapper.GrpcToDto(request)
            }, context.CancellationToken);

            return NomenclatureMapper.DtoToGrpc(response.Nomenclature);
        }

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

        public override async Task<NomenclatureGrpc> Update(NomenclatureGrpc request, ServerCallContext context)
        {
            UpdateNomenclatureQueryResponse response = await _mediator.Send(new UpdateNomenclatureQuery()
            {
                Nomenclature = NomenclatureMapper.GrpcToDto(request)
            }, context.CancellationToken);

            return NomenclatureMapper.DtoToGrpc(response.Nomenclature);
        }
    }
}