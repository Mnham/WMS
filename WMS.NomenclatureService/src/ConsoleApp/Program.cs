using Dapper;

using System.Collections.Generic;

using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NomenclatureFilter filter = new()
            {
                NomenclatureId = null,
                NomenclatureName = "123",
                NomenclatureTypeId = 20
            };

            const string sql = @"
                SELECT *
                FROM nomenclature
                INNER JOIN nomenclature_type ON nomenclature.nomenclature_type_id = nomenclature_type.id
                /**where**/;";

            SqlBuilder builder = new();
            SqlBuilder.Template select = builder.AddTemplate(sql);
            DynamicParameters parameter = new();
            IReadOnlyList<FilterParameter> filters = FilterParameter.GetFilters(filter);

            foreach (FilterParameter f in filters)
            {
                parameter.Add(f.ParameterName, f.Value);
                builder.Where($"{f.SqlField} {f.SqlOperator} @{f.ParameterName}");
            }
        }
    }

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