using FluentMigrator;

namespace WMS.NomenclatureService.Migrator.Migrations
{
    /// <summary>
    /// ������������ �������� ��� �������� ������� �������������.
    /// </summary>
    [Migration(1)]
    public class EmployeeTable : Migration
    {
        /// <summary>
        /// ��������� ��������.
        /// </summary>
        public override void Up() => Create
                .Table("employee")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

        /// <summary>
        /// ���������� ��������.
        /// </summary>
        public override void Down() => Delete.Table("employee");
    }
}