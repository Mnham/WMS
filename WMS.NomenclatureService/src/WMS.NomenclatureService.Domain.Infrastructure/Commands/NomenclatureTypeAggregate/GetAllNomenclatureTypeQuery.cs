using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate
{
    public class GetAllNomenclatureTypeQuery : IRequest<GetAllNomenclatureTypeQueryResponse>
    {
    }
}