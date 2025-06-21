using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Zeal.Infra.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTableBase(string table) // protected faz com que apenas as classes que herdam possam acessar os métodos
    {
        return Create.Table(table)
            .WithColumn("Id").AsInt64().PrimaryKey().Identity() // Identity column for auto-incrementing primary key
            .WithColumn("Active").AsBoolean().NotNullable()
            .WithColumn("CreatedOn").AsDateTime().NotNullable();
    }
}
