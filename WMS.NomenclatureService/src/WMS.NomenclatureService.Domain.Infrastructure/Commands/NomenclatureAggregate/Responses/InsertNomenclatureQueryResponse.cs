using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses
{
    public class InsertNomenclatureQueryResponse
    {
        public NomenclatureDto Nomenclature { get; init; }
    }
}
