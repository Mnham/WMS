using FluentMigrator;

namespace WMS.NomenclatureService.Migrator.Migrations
{
    [Migration(1)]
    public class NomenclatureTypeTable : Migration
    {
        public override void Up() => Create
                .Table("nomenclature_type")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

        public override void Down() => Delete.Table("nomenclature_type");
    }
}