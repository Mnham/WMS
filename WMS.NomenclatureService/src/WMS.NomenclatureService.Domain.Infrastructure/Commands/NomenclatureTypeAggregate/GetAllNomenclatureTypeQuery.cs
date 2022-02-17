using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate.Responses;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureTypeAggregate
{
    /// <summary>
    /// Представляет команду для запроса всех типов номенклатур.
    /// </summary>
    public class GetAllNomenclatureTypeQuery : IRequest<GetAllNomenclatureTypeQueryResponse>
    {
    }
}