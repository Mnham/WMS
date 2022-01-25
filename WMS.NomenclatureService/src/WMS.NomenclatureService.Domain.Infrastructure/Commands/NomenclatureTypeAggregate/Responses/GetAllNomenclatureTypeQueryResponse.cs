using System.Collections.Generic;

using WMS.ClassLibrary.Infrastructure.Models;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses
{
    /// <summary>
    /// Представляет ответ на запрос всех типов номенклатур.
    /// </summary>
    public class GetAllNomenclatureTypeQueryResponse : IItemsModel<NomenclatureTypeDto>
    {
        /// <summary>
        /// Список типов номенклатур.
        /// </summary>
        public IReadOnlyList<NomenclatureTypeDto> Items { get; set; }
    }
}