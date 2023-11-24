
using FluentMigrator;

[Migration(20231121)]
public class AddProductsTable : Migration
{
    public override void Up()
    {
        Create.Table("Message")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("senderId").AsGuid().NotNullable()
            .WithColumn("message").AsCustom("NVARCHAR(MAX)").NotNullable()
            .WithColumn("room").AsString(500).NotNullable()
            .WithColumn("createdDate").AsDateTime().NotNullable();
        Create.Table("User")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("email").AsString(50).NotNullable()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("password").AsString(300).NotNullable()
            .WithColumn("image").AsCustom("NVARCHAR(MAX)").NotNullable()
            .WithColumn("createdDate").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Message");
        Delete.Table("User");
    }
}