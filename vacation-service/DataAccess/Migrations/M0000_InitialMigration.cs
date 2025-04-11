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
            .WithColumn("image_name").AsString().Nullable()
            .WithColumn("hashed_password").AsString().NotNullable()
            .WithColumn("phone").AsString().Nullable()
            .WithColumn("email").AsString().NotNullable().Unique()
            .WithColumn("telegram_username").AsString().Nullable()
            .WithColumn("department_id").AsGuid().Nullable()
            .WithColumn("role").AsString().NotNullable();
        
        Create.Table("departments")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("supervisor_id").AsGuid().NotNullable()
            .WithColumn("company_id").AsGuid().NotNullable()
            .WithColumn("image_name").AsString().Nullable();
        
        Create.Table("schemas")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("name").AsString().NotNullable();
        
        Create.Table("steps")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .WithColumn("schema_id").AsGuid().NotNullable()
            .WithColumn("approver_id").AsGuid().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("departments");
        Delete.Table("steps");
        Delete.Table("schemas");
    }
}