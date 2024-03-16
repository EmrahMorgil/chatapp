
using FluentMigrator;

[Migration(20231121)]
public class AddProductsTable : Migration
{
    public override void Up()
    {
        Create.Table("tbl_message")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("created_date").AsDateTime().NotNullable()
            .WithColumn("sender_id").AsGuid().NotNullable()
            .WithColumn("content").AsString(int.MaxValue).NotNullable()
            .WithColumn("room").AsString(500).NotNullable();
        Create.Table("tbl_user")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("created_date").AsDateTime().NotNullable()
            .WithColumn("email").AsString(50).NotNullable()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("password").AsString(300).NotNullable()
            .WithColumn("image").AsString(int.MaxValue).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("tbl_message");
        Delete.Table("tbl_user");
    }
}