using FluentMigrator;

namespace DataAccess.Migrations;

[Migration(0)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("surname").AsString().NotNullable()
            .WithColumn("patronymic").AsString().Nullable()
            .WithColumn("image_id").AsGuid().Nullable()
            .WithColumn("hashed_password").AsString().NotNullable()
            .WithColumn("phone").AsString().Nullable()
            .WithColumn("email").AsString().NotNullable().Unique()
            .WithColumn("telegram_username").AsString().Nullable()
            .WithColumn("department_id").AsGuid().Nullable()
            .WithColumn("role").AsString().Nullable(); 
        
        Create.Table("departments")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("supervisor_id").AsGuid().NotNullable().Unique();
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("departments");
    }
}