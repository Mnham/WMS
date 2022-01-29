using FluentMigrator;

namespace WMS.NomenclatureService.Migrator.Migrations
{
    [Migration(1)]
    public class EmployeeTable : Migration
    {
        public override void Up() => Create
                .Table("employee")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

        public override void Down() => Delete.Table("employee");
    }
}