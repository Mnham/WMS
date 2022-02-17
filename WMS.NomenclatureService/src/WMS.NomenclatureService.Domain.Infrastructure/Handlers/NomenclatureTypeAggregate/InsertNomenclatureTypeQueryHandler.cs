using MediatR;

using System.Threading;
using System.Threading.Tasks;

using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureTypeAggregate
{
    /// <summary>
    /// Представляет обработчик команды добавления типа номенклатуры.
    /// </summary>
    public class InsertNomenclatureTypeQueryHandler : IRequestHandler<InsertNomenclatureTypeQuery, InsertNomenclatureTypeQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория типа номенклатуры.
        /// </summary>
        private readonly INomenclatureTypeRepository _nomenclatureTypeRepository;

        /// <summary>
        /// Создает экземпляр класса <see cref="InsertNomenclatureTypeQueryHandler"/>.
        /// </summary>
        public InsertNomenclatureTypeQueryHandler(INomenclatureTypeRepository nomenclatureTypeRepository) =>
            _nomenclatureTypeRepository = nomenclatureTypeRepository;

        /// <summary>
        /// Обрабатывает команду добавления типа номенклатуры.
        /// </summary>
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