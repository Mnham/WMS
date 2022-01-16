using FluentMigrator;

namespace WMS.NomenclatureService.Migrator.Migrations
{
    [Migration(2)]
    public class NomenclatureTable : Migration
    {
        public override void Up() => Create
                .Table("nomenclature")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("nomenclature_type_id").AsInt64().NotNullable()
                .WithColumn("length").AsInt64().NotNullable()
                .WithColumn("width").AsInt64().NotNullable()
                .WithColumn("height").AsInt64().NotNullable()
                .WithColumn("weight").AsInt32().NotNullable();

        public override void Down() => Delete.Table("nomenclature");
    }
}