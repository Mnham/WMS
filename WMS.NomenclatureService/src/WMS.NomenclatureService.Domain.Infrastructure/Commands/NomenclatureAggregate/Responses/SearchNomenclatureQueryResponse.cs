using System.Collections.Generic;

using WMS.Microservice.Infrastructure.Models;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос поиска номенклатуры.
    /// </summary>
    public class SearchNomenclatureQueryResponse : IItemsModel<NomenclatureDto>
    {
        /// <summary>
        /// Список номенклатур.
        /// </summary>
        public IReadOnlyList<NomenclatureDto> Items { get; set; }
    }
}