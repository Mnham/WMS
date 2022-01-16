using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.ClassLibrary.Extensions;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureAggregate
{
    public class SearchNomenclatureQueryHandler : IRequestHandler<SearchNomenclatureQuery, SearchNomenclatureQueryResponse>
    {
        private readonly INomenclatureRepository _nomenclatureRepository;

        public SearchNomenclatureQueryHandler(INomenclatureRepository nomenclatureRepository) =>
            _nomenclatureRepository = nomenclatureRepository;

        public async Task<SearchNomenclatureQueryResponse> Handle(SearchNomenclatureQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Nomenclature> nomenclatures = await _nomenclatureRepository.Search(
                new NomenclatureFilter()
                {
                    NomenclatureId = request.NomenclatureId,
                    NomenclatureName = request.NomenclatureName,
                    NomenclatureTypeId = request.NomenclatureTypeId
                }, cancellationToken);

            return new SearchNomenclatureQueryResponse()
            {
                Items = nomenclatures.Map(NomenclatureMapper.EntityToDto).ToList()
            };
        }
    }
}