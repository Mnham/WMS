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
    /// Представляет обработчик команды обновления номенклатуры.
    /// </summary>
    public class UpdateNomenclatureQueryHandler : IRequestHandler<UpdateNomenclatureQuery, UpdateNomenclatureQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория номенклатуры.
        /// </summary>
        private readonly INomenclatureRepository _nomenclatureRepository;

        /// <summary>
        /// Создает экземпляр класса <see cref="UpdateNomenclatureQueryHandler"/>.
        /// </summary>
        public UpdateNomenclatureQueryHandler(INomenclatureRepository nomenclatureRepository) =>
            _nomenclatureRepository = nomenclatureRepository;

        /// <summary>
        /// Обрабатывает команду обновления номенклатуры.
        /// </summary>
        public async Task<UpdateNomenclatureQueryResponse> Handle(UpdateNomenclatureQuery request, CancellationToken cancellationToken)
        {
            Nomenclature nomenclature = await _nomenclatureRepository.Update(
                NomenclatureMapper.DtoToEntity(request.Nomenclature),
                cancellationToken);

            return new UpdateNomenclatureQueryResponse()
            {
                Nomenclature = NomenclatureMapper.EntityToDto(nomenclature)
            };
        }
    }
}