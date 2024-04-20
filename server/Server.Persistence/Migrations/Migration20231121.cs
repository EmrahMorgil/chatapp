
using FluentMigrator;

[Migration(20231121)]
public class AddProductsTable : Migration
{
    public override void Up()
    {
        Create.Table("Messages")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("createdDate").AsDateTime().NotNullable()
            .WithColumn("senderId").AsGuid().NotNullable()
            .WithColumn("content").AsString(int.MaxValue).NotNullable()
            .WithColumn("room").AsString(500).NotNullable();
        Create.Table("Users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("createdDate").AsDateTime().NotNullable()
            .WithColumn("email").AsString(50).NotNullable()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("password").AsString(300).NotNullable()
            .WithColumn("image").AsString(int.MaxValue).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Messages");
        Delete.Table("Users");
    }
}