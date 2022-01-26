using FluentMigrator;

namespace WMS.NomenclatureService.Migrator.Migrations
{
  /// <summary>
  /// ������������ �������� ��� �������� ������� ���� ������������.
  /// </summary>
    [Migration(1)]
    public class NomenclatureTypeTable : Migration
    {
        /// <summary>
        /// ��������� ��������.
        /// </summary>
        public override void Up() => Create
                .Table("nomenclature_type")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

        /// <summary>
        /// ���������� ��������.
        /// </summary>
        public override void Down() => Delete.Table("nomenclature_type");
    }
}