using Microsoft.EntityFrameworkCore.Migrations;

namespace ThedoorCode.Migrations
{
    public partial class UpdateDbExperience300 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_UserModels_UserId1",
                table: "Experiences");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Experiences",
                newName: "UserModel");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_UserId1",
                table: "Experiences",
                newName: "IX_Experiences_UserModel");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_UserModels_UserModel",
                table: "Experiences",
                column: "UserModel",
                principalTable: "UserModels",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_UserModels_UserModel",
                table: "Experiences");

            migrationBuilder.RenameColumn(
                name: "UserModel",
                table: "Experiences",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_UserModel",
                table: "Experiences",
                newName: "IX_Experiences_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_UserModels_UserId1",
                table: "Experiences",
                column: "UserId1",
                principalTable: "UserModels",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
