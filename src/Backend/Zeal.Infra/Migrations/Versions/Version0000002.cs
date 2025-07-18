using FluentMigrator;

namespace Zeal.Infra.Migrations.Versions;


[Migration(DatabaseVersions.TABLE_EVENT, "Create table to save the events informations")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTableBase("Addresses")
            .WithColumn("Location").AsString(200).NotNullable()
            .WithColumn("City").AsString(50).NotNullable()
            .WithColumn("State").AsString(50).NotNullable()
            .WithColumn("Country").AsString(50).NotNullable();

        CreateTableBase("Events")
            // Informações básicas
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Description").AsString().Nullable()
            .WithColumn("StartDate").AsDateTime2().NotNullable()
            .WithColumn("AddressId").AsInt64().NotNullable().ForeignKey("FK_Event_For_Address_Id", "Addresses", "Id") // ID do endereço, referenciando a tabela de endereços - 1 pra 1 DONA
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Event_For_User_Id", "Users", "Id")
            .OnDelete(System.Data.Rule.Cascade)// ID do usuário que criou o evento, referenciando a tabela de usuários - 1 pra muitos 

            // Configurações da corrida
            .WithColumn("Distance").AsDecimal().NotNullable() // Em quilômetros (ex: 5.0, 21.1)
            .WithColumn("MaxParticipants").AsInt32().Nullable()
            .WithColumn("MinimumAge").AsInt32().Nullable()

            // Inscrições e pagamento
            .WithColumn("RegistrationPrice").AsDecimal().NotNullable()
            .WithColumn("RegistrationStartDate").AsDateTime2().NotNullable()
            .WithColumn("RegistrationEndDate").AsDateTime2().Nullable();

        //Em casos de 1 pra 1 a chave estrangeira deve ser criada na entidade "dona". Em casos de 1 pra muitos, a chave estrangeira deve ser criada na entidade que possui o relacionamento "muitos".
    }
}