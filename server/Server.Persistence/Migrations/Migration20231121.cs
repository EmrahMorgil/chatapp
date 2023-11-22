
using FluentMigrator;

[Migration(20231121)]
public class AddProductsTable : Migration
{
    public override void Up()
    {
        Create.Table("Message")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("senderId").AsGuid().NotNullable()
            .WithColumn("message").AsString(500).NotNullable()
            .WithColumn("room").AsGuid().NotNullable()
            .WithColumn("createdDate").AsDateTime().NotNullable();
        Create.Table("User")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("email").AsString(50).NotNullable()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("password").AsString(200).NotNullable()
            .WithColumn("image").AsString(500).NotNullable()
            .WithColumn("createdDate").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Message");
        Delete.Table("User");
    }
}