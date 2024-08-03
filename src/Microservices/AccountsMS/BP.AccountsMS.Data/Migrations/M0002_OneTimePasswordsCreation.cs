using FluentMigrator;

namespace BP.AccountsMS.Data.Migrations
{
    [Migration(2, "Create OneTimePasswords")]
    public class M0002_OneTimePasswordsCreation : Migration
    {
        public override void Up()
        {
            Create.Table("OneTimePasswords")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("AccountId").AsGuid().NotNullable().ForeignKey("Accounts", "Id")
                .WithColumn("Password").AsString(150).NotNullable()
                .WithColumn("CreatedAtUtc").AsDateTimeOffset(7).NotNullable()
                .WithColumn("ExpiredAtUtc").AsDateTimeOffset(7).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("OneTimePasswords");
        }
    }
}
