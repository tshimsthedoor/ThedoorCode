using Microsoft.EntityFrameworkCore.Migrations;

namespace ThedoorCode.Migrations
{
    public partial class Isdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "UserModels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "UserModels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "UserModels");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "UserModels");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Experiences");
        }
    }
}
