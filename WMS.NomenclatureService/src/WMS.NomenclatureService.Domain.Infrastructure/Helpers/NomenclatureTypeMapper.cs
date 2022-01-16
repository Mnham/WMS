using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureTypeAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Models;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.Domain.Infrastructure.Helpers
{
    public static class NomenclatureTypeMapper
    {
        public static NomenclatureTypeGrpc DtoToGrpc(NomenclatureTypeDto item) => new()
        {
            Id = item.Id,
            Name = item.Name
        };

        public static NomenclatureTypeDto EntityToDto(NomenclatureType item) => new()
        {
            Id = item.Id,
            Name = item.Name
        };

        internal static NomenclatureType DtoToEntity(NomenclatureTypeDto item) => new
        (
            item.Id,
            item.Name
        );
    }
}