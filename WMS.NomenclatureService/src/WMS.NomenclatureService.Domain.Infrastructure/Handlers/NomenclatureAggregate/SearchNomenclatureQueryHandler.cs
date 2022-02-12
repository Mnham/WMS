using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.Microservice.Extensions;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureAggregate
{
    /// <summary>
    /// Представляет обработчик команды поиска номенклатуры.
    /// </summary>
    public class SearchNomenclatureQueryHandler : IRequestHandler<SearchNomenclatureQuery, SearchNomenclatureQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория номенклатуры.
        /// </summary>
        private readonly INomenclatureRepository _nomenclatureRepository;

        /// <summary>
        /// Создает экземпляр класса <see cref="SearchNomenclatureQueryHandler"/>.
        /// </summary>
        public SearchNomenclatureQueryHandler(INomenclatureRepository nomenclatureRepository) =>
            _nomenclatureRepository = nomenclatureRepository;

        /// <summary>
        /// Обрабатывает команду поиска номенклатуры.
        /// </summary>
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