using System.Collections.Generic;

using WMS.ClassLibrary.Infrastructure.Models;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses
{
    public class GetAllNomenclatureTypeQueryResponse : IItemsModel<NomenclatureTypeDto>
    {
        public IReadOnlyList<NomenclatureTypeDto> Items { get; set; }
    }
}