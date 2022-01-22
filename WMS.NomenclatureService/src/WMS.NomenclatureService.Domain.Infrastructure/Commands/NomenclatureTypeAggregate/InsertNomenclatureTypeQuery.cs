using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate
{
    public class InsertNomenclatureTypeQuery : IRequest<InsertNomenclatureTypeQueryResponse>
    {
        public NomenclatureTypeDto NomenclatureType { get; init; }
    }
}
