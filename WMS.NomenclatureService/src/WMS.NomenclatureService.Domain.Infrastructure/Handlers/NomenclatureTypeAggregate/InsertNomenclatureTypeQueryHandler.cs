using MediatR;

using System.Threading;
using System.Threading.Tasks;

using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureTypeAggregate
{
    public class InsertNomenclatureTypeQueryHandler : IRequestHandler<InsertNomenclatureTypeQuery, InsertNomenclatureTypeQueryResponse>
    {
        private readonly INomenclatureTypeRepository _nomenclatureTypeRepository;

        public InsertNomenclatureTypeQueryHandler(INomenclatureTypeRepository nomenclatureTypeRepository) =>
            _nomenclatureTypeRepository = nomenclatureTypeRepository;

        public async Task<InsertNomenclatureTypeQueryResponse> Handle(InsertNomenclatureTypeQuery request, CancellationToken cancellationToken)
        {
            NomenclatureType nomenclatureType = await _nomenclatureTypeRepository.Insert(
                NomenclatureTypeMapper.DtoToEntity(request.NomenclatureType),
                cancellationToken);

            return new InsertNomenclatureTypeQueryResponse()
            {
                NomenclatureType = NomenclatureTypeMapper.EntityToDto(nomenclatureType)
            };
        }
    }
}