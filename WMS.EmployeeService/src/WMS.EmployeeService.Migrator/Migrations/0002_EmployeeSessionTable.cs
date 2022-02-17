using FluentMigrator;

namespace WMS.EmployeeService.Migrator.Migrations
{
    /// <summary>
    /// Представляет миграцию для таблицы сессий.
    /// </summary>
    [Migration(2)]
    public class EmployeeSessionTable : Migration
    {
        /// <summary>
        /// Применяет миграцию.
        /// </summary>
        public override void Up() => Create
            .Table("employee_session")
            .WithColumn("id").AsInt64().Identity().PrimaryKey()
            .WithColumn("employee_id").AsInt64().NotNullable()
            .WithColumn("task_type_id").AsInt64().NotNullable()
            .WithColumn("Equipment_id").AsInt64().NotNullable();

        /// <summary>
        /// Откатывает миграцию.
        /// </summary>
        public override void Down() => Delete.Table("employee_session");
    }
}