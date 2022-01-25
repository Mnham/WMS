using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос обновления типа номенклатуры.
    /// </summary>
    public class UpdateNomenclatureTypeQueryResponse
    {
        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        public NomenclatureTypeDto NomenclatureType { get; init; }
    }
}
