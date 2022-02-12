using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate
{
    /// <summary>
    /// Представляет параметры фильтра номенклатуры.
    /// </summary>
    public class NomenclatureFilter
    {
        /// <summary>
        /// Идентификатор номенклатуры.
        /// </summary>
        [SqlField("nomenclature.id"), SqlOperator("=")]
        public long? NomenclatureId { get; set; }

        /// <summary>
        /// Наименование номенклатуры.
        /// </summary>
        [SqlField("nomenclature.name"), SqlOperator("LIKE")]
        public string NomenclatureName { get; set; }

        /// <summary>
        /// Идентификатор типа номенклатуры.
        /// </summary>
        [SqlField("nomenclature_type.id"), SqlOperator("=")]
        public long? NomenclatureTypeId { get; set; }
    }
}