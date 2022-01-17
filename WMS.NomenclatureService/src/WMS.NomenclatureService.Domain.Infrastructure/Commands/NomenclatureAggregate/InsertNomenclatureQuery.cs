using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate
{
    public class InsertNomenclatureQuery : IRequest<InsertNomenclatureQueryResponse>
    {
        public NomenclatureDto Nomenclature { get; init; }
    }
}
