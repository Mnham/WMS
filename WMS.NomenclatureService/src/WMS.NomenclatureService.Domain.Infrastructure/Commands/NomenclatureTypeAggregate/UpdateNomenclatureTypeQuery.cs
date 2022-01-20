using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;
using WMS.NomenclatureService.Domain.Infrastructure.Models;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate
{
    public class UpdateNomenclatureTypeQuery : IRequest<UpdateNomenclatureTypeQueryResponse>
    {
        public NomenclatureTypeDto NomenclatureType { get; init; }
    }
}
