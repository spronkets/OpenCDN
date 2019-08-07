using FluentMigrator;

namespace OpenCdn.Data.Migrations
{
    [Migration(MigrationVersions.CreateInitialSchema)]
    public class CreateInitialSchema : Migration
    {
        /// <summary>
        /// Upgrade to this Migration.
        /// </summary>
        public override void Up()
        {
            // Create User Tables
            CreateUsersTable();
            CreateUserLoginAttemptsTable();

            // Create File Tables
            CreateFileGroupsTable();
            CreateFilesTable();
        }

        /// <summary>
        /// Downgrade to the Migration before this. Should be in the reverse order of the upgrade.
        /// </summary>
        public override void Down()
        {
            // Delete File Tables
            Delete.Table("UserLoginAttempts");
            Delete.Table("Users");

            // Delete User Tables
            Delete.Table("Files");
            Delete.Table("FileGroups");
        }

        #region Create User Tables

        private void CreateUsersTable()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().PrimaryKey("PK_User_Id").Identity()
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Email").AsString().NotNullable().Unique("UK_User_Email")
                .WithColumn("DisplayName").AsString().NotNullable().Unique("UK_User_DisplayName")
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }

        private void CreateUserLoginAttemptsTable()
        {
            Create.Table("UserLoginAttempts")
                .WithColumn("Id").AsInt64().PrimaryKey("PK_UserLoginAttempt_Id").Identity()
                .WithColumn("UserId").AsInt64().ForeignKey("FK_UserLoginAttempt_UserId", "Users", "Id")
                .WithColumn("ClientIp").AsString().NotNullable()
                .WithColumn("AttemptDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }

        #endregion Create User Tables

        #region Create File Tables

        private void CreateFileGroupsTable()
        {
            Create.Table("FileGroups")
                .WithColumn("Id").AsInt64().PrimaryKey("PK_FileGroup_Id").Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("HomeUrl").AsString().Nullable()
                .WithColumn("RepositoryUrl").AsString().Nullable()
                .WithColumn("CreatedBy").AsInt64().NotNullable().ForeignKey("FK_FileGroup_CreatedById", "Users", "Id")
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }

        private void CreateFilesTable()
        {
            Create.Table("Files")
                .WithColumn("Id").AsInt64().PrimaryKey("PK_File_Id").Identity()
                .WithColumn("GroupId").AsInt64().ForeignKey("FK_File_GroupId", "FileGroups", "Id")
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Version").AsString().Nullable()
                .WithColumn("CreatedBy").AsInt64().NotNullable().ForeignKey("FK_File_CreatedById", "Users", "Id")
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("LastModifiedDate").AsDateTime().Nullable();
        }

        #endregion Create File Tables
    }
}
