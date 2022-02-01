using FluentMigrator;

namespace WMS.NomenclatureService.Migrator.Migrations
{
    /// <summary>
    /// Представляет миграцию для создания таблицы пользователей.
    /// </summary>
    [Migration(1)]
    public class EmployeeTable : Migration
    {
        /// <summary>
        /// Применяет миграцию.
        /// </summary>
        public override void Up() => Create
                .Table("employee")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

        /// <summary>
        /// Откатывает миграцию.
        /// </summary>
        public override void Down() => Delete.Table("employee");
    }
}