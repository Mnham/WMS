using FluentMigrator;

namespace WMS.NomenclatureService.Migrator.Migrations
{
  /// <summary>
  /// Представляет миграцию для создания таблицы типа номенклатуры.
  /// </summary>
    [Migration(1)]
    public class NomenclatureTypeTable : Migration
    {
        /// <summary>
        /// Применяет миграцию.
        /// </summary>
        public override void Up() => Create
                .Table("nomenclature_type")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

        /// <summary>
        /// Откатывает миграцию.
        /// </summary>
        public override void Down() => Delete.Table("nomenclature_type");
    }
}