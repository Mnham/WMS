using System.Collections.Generic;

using WMS.ClassLibrary.Infrastructure.Models;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses
{
    public class SearchNomenclatureQueryResponse : IItemsModel<NomenclatureDto>
    {
        public IReadOnlyList<NomenclatureDto> Items { get; set; }
    }
}