using MediatR;

using System.Threading;
using System.Threading.Tasks;

using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureAggregate
{
    public class InsertNomenclatureQueryHandler : IRequestHandler<InsertNomenclatureQuery, InsertNomenclatureQueryResponse>
    {
        private readonly INomenclatureRepository _nomenclatureRepository;

        public InsertNomenclatureQueryHandler(INomenclatureRepository nomenclatureRepository) =>
            _nomenclatureRepository = nomenclatureRepository;

        public async Task<InsertNomenclatureQueryResponse> Handle(InsertNomenclatureQuery request, CancellationToken cancellationToken)
        {
            Nomenclature nomenclature = await _nomenclatureRepository.Insert(
                NomenclatureMapper.DtoToEntity(request.Nomenclature),
                cancellationToken);

            return new InsertNomenclatureQueryResponse()
            {
                Nomenclature = NomenclatureMapper.EntityToDto(nomenclature)
            };
        }
    }
}