using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.NomenclatureService.Domain.AggregationModels.NomenclatureAggregate
{
    public class NomenclatureFilter
    {
        [SqlField("nomenclature.id"), SqlOperator("=")]
        public long? NomenclatureId { get; set; }

        [SqlField("nomenclature.name"), SqlOperator("LIKE")]
        public string NomenclatureName { get; set; }

        [SqlField("nomenclature_type.id"), SqlOperator("=")]
        public long? NomenclatureTypeId { get; set; }
    }
}