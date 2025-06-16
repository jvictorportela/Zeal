using FluentMigrator;

namespace Zeal.Infra.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER, "Create table to save the user's informations")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTableBase("User")
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("Password").AsString(255).NotNullable();
    }
}