using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate
{
    /// <summary>
    /// Предствляет команду обновления типа номенклатуры.
    /// </summary>
    public class UpdateNomenclatureTypeQuery : IRequest<UpdateNomenclatureTypeQueryResponse>
    {
        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        public NomenclatureTypeDto NomenclatureType { get; init; }
    }
}
