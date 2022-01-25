using MediatR;

using System.Threading;
using System.Threading.Tasks;

using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureAggregate
{
    /// <summary>
    /// Представляет обработчик команды добавления номенклатуры.
    /// </summary>
    public class InsertNomenclatureQueryHandler : IRequestHandler<InsertNomenclatureQuery, InsertNomenclatureQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория номенклатуры.
        /// </summary>
        private readonly INomenclatureRepository _nomenclatureRepository;

        /// <summary>
        /// Создает экземпляр класса <see cref="InsertNomenclatureQueryHandler"/>.
        /// </summary>
        public InsertNomenclatureQueryHandler(INomenclatureRepository nomenclatureRepository) =>
            _nomenclatureRepository = nomenclatureRepository;

        /// <summary>
        /// Обрабатывает команду добавления номенклатуры.
        /// </summary>
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