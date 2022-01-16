using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Models;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.Domain.Infrastructure.Helpers
{
    public static class NomenclatureMapper
    {
        public static NomenclatureGrpc DtoToGrpc(NomenclatureDto item) => new()
        {
            Id = item.Id,
            Type = NomenclatureTypeMapper.DtoToGrpc(item.Type),
            Name = item.Name,
            Length = item.Length,
            Width = item.Width,
            Height = item.Height,
            Weight = item.Weight
        };

        public static NomenclatureDto EntityToDto(Nomenclature item) => new()
        {
            Id = item.Id,
            Type = NomenclatureTypeMapper.EntityToDto(item.Type),
            Name = item.Name,
            Length = item.Length,
            Width = item.Width,
            Height = item.Height,
            Weight = item.Weight
        };

        public static Nomenclature DtoToEntity(NomenclatureDto item) => new
        (
            item.Id,
            item.Name,
            NomenclatureTypeMapper.DtoToEntity(item.Type),
            item.Length,
            item.Width,
            item.Height,
            item.Weight
        );
    }
}