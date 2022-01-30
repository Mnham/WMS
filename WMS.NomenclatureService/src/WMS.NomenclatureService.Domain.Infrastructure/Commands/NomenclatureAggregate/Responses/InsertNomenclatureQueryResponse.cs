using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос добавления номенклатуры.
    /// </summary>
    public class InsertNomenclatureQueryResponse
    {
        /// <summary>
        /// Номенклатура.
        /// </summary>
        public NomenclatureDto Nomenclature { get; init; }
    }
}
