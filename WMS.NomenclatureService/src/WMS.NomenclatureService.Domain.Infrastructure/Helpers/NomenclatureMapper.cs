using WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate;
using WMS.NomenclatureService.Domain.Infrastructure.Models;
using WMS.NomenclatureService.Grpc;

namespace WMS.NomenclatureService.Domain.Infrastructure.Helpers
{
    /// <summary>
    /// Представляет методы маппинга номенклатуры.
    /// </summary>
    public static class NomenclatureMapper
    {
        /// <summary>
        /// Возвращает экземпляр Grpc, преобразованный из Dto.
        /// </summary>
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

        /// <summary>
        /// Возвращает экземпляр Dto, преобразованный из Grpc.
        /// </summary>
        public static NomenclatureDto GrpcToDto(NomenclatureGrpc item) => new()
        {
            Id = item.Id,
            Type = NomenclatureTypeMapper.GrpcToDto(item.Type),
            Name = item.Name,
            Length = item.Length,
            Width = item.Width,
            Height = item.Height,
            Weight = item.Weight
        };

        /// <summary>
        /// Возвращает экземпляр Dto, преобразованный из Entity.
        /// </summary>
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

        /// <summary>
        /// Возвращает экземпляр Entity, преобразованный из Dto.
        /// </summary>
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