using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVG_TASK_APP.Migrations
{
    /// <inheritdoc />
    public partial class fixdb : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "User Workspaces");

            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Task");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User Workspaces",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Activity",
                table: "Task",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
