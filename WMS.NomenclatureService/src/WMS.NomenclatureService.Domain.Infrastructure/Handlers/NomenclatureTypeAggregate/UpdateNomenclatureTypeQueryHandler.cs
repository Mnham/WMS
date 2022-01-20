using MediatR;

using System.Threading;
using System.Threading.Tasks;

using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureTypeAggregate
{
    public class UpdateNomenclatureTypeQueryHandler : IRequestHandler<UpdateNomenclatureTypeQuery, UpdateNomenclatureTypeQueryResponse>
    {
        private readonly INomenclatureTypeRepository _nomenclatureTypeRepository;

        public UpdateNomenclatureTypeQueryHandler(INomenclatureTypeRepository nomenclatureTypeRepository) =>
            _nomenclatureTypeRepository = nomenclatureTypeRepository;

        public async Task<UpdateNomenclatureTypeQueryResponse> Handle(UpdateNomenclatureTypeQuery request, CancellationToken cancellationToken)
        {
            NomenclatureType nomenclatureType = await _nomenclatureTypeRepository.Update(
                NomenclatureTypeMapper.DtoToEntity(request.NomenclatureType),
                cancellationToken);

            return new UpdateNomenclatureTypeQueryResponse()
            {
                NomenclatureType = NomenclatureTypeMapper.EntityToDto(nomenclatureType)
            };
        }
    }
}