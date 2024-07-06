using FluentMigrator;

[Migration(20231121)]
public class AddProductsTable : Migration
{
    public override void Up()
    {
        Create.Table("Messages")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("CreatedDate").AsDateTime().NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("Content").AsString(int.MaxValue).NotNullable()
            .WithColumn("Room").AsString(500).NotNullable();
        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("CreatedDate").AsDateTime().NotNullable()
            .WithColumn("Email").AsString(50).NotNullable()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Password").AsString(300).NotNullable()
            .WithColumn("Image").AsString(int.MaxValue).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Messages");
        Delete.Table("Users");
    }
}