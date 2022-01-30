using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос добавления типа номенклатуры.
    /// </summary>
    public class InsertNomenclatureTypeQueryResponse
    {
        /// <summary>
        /// Тип номенклатуры.
        /// </summary>
        public NomenclatureTypeDto NomenclatureType { get; init; }
    }
}
