using Microsoft.EntityFrameworkCore.Migrations;

namespace POC_GMS_API.DataAccess.Migrations
{
    public partial class updateapplicationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Application");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Application");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Application",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
