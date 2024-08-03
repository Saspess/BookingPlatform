using FluentMigrator;

namespace BP.AccountsMS.Data.Migrations
{
    [Migration(1, "Create Users")]
    public class M0001_UsersCreation : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).Nullable()
                .WithColumn("Email").AsString(50).NotNullable()
                .WithColumn("IsEmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("Role").AsString(10).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
