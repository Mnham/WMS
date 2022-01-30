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
    /// Представляет обработчик команды обновления типа номенклатуры.
    /// </summary>
    public class UpdateNomenclatureTypeQueryHandler : IRequestHandler<UpdateNomenclatureTypeQuery, UpdateNomenclatureTypeQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория типа номенклатуры.
        /// </summary>
        private readonly INomenclatureTypeRepository _nomenclatureTypeRepository;

        /// <summary>
        /// Создает экземпляр класса <see cref="UpdateNomenclatureTypeQueryHandler"/>.
        /// </summary>
        public UpdateNomenclatureTypeQueryHandler(INomenclatureTypeRepository nomenclatureTypeRepository) =>
            _nomenclatureTypeRepository = nomenclatureTypeRepository;

        /// <summary>
        /// Обрабатывает команду обновления типа номенклатуры.
        /// </summary>
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