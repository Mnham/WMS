using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос обновления номенклатуры.
    /// </summary>
    public class UpdateNomenclatureQueryResponse
    {
        /// <summary>
        /// Номенклатура.
        /// </summary>
        public NomenclatureDto Nomenclature { get; init; }
    }
}
