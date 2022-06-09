using Microsoft.EntityFrameworkCore.Migrations;

namespace ThedoorCode.Migrations
{
    public partial class UserID_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserModels");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "UserModels");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserModels");
        }
    }
}
