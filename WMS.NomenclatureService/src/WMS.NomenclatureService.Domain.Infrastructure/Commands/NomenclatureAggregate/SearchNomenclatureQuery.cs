using MediatR;

using WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate.Responses;

namespace WMS.NomenclatureService.Domain.Infrastructure.Commands.NomenclatureAggregate
{
    public class SearchNomenclatureQuery : IRequest<SearchNomenclatureQueryResponse>
    {
        public long? NomenclatureId { get; set; }
        public string NomenclatureName { get; set; }
        public long? NomenclatureTypeId { get; set; }
    }
}