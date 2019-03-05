using FluentMigrator;

namespace OpenCdn.Data.Migrations
{
    [Migration(MigrationVersions.CreateInitialSchema)]
    public class CreateInitialSchema : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().PrimaryKey("PK_Users_Id").Identity()
                .WithColumn("Email").AsString().NotNullable().Unique("UK_Users_Email")
                .WithColumn("DisplayName").AsString().Nullable().WithDefaultValue(null)
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);

            Create.Table("Files")
                .WithColumn("Id").AsInt64().PrimaryKey("PK_Files_Id").Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Location").AsString().NotNullable()
                .WithColumn("CreatedBy").AsInt64().NotNullable().ForeignKey("FK_Files_UserId", "Users", "Id")
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("ModifiedDate").AsDateTime().Nullable();

            Create.UniqueConstraint("UK_Files_CreatedBy_Name")
                .OnTable("Files")
                .Columns("CreatedBy", "Name");
        }

        public override void Down()
        {
            Delete.Table("Files");
            Delete.Table("Users");
        }
    }
}
