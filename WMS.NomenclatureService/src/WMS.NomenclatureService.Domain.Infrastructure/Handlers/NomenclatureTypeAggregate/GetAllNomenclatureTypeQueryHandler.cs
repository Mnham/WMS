using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.Microservice.Extensions;
using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Helpers;

namespace WMS.NomenclatureService.Domain.Infrastructure.Handlers.NomenclatureTypeAggregate
{
    /// <summary>
    /// Представляет обработчик команды запроса всех типов номенклатур.
    /// </summary>
    public class GetAllNomenclatureTypeQueryHandler : IRequestHandler<GetAllNomenclatureTypeQuery, GetAllNomenclatureTypeQueryResponse>
    {
        /// <summary>
        /// Экземпляр репозитория типа номенклатуры.
        /// </summary>
        private readonly INomenclatureTypeRepository _nomenclatureTypeRepository;

        /// <summary>
        /// Создает экземпляр класса <see cref="GetAllNomenclatureTypeQueryHandler"/>.
        /// </summary>
        public GetAllNomenclatureTypeQueryHandler(INomenclatureTypeRepository nomenclatureTypeRepository) =>
            _nomenclatureTypeRepository = nomenclatureTypeRepository;

        /// <summary>
        /// Обрабатывает команду запроса всех типов номенклатур.
        /// </summary>
        public async Task<GetAllNomenclatureTypeQueryResponse> Handle(GetAllNomenclatureTypeQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<NomenclatureType> types = await _nomenclatureTypeRepository.GetAll(cancellationToken);

            return new GetAllNomenclatureTypeQueryResponse()
            {
                Items = types.Map(NomenclatureTypeMapper.EntityToDto).ToList()
            };
        }
    }
}