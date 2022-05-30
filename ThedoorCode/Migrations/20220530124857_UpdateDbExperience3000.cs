using Microsoft.EntityFrameworkCore.Migrations;

namespace ThedoorCode.Migrations
{
    public partial class UpdateDbExperience3000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_UserModels_UserModel",
                table: "Experiences");

            migrationBuilder.RenameColumn(
                name: "UserModel",
                table: "Experiences",
                newName: "UserModelUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_UserModel",
                table: "Experiences",
                newName: "IX_Experiences_UserModelUserId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Experiences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_UserModels_UserModelUserId",
                table: "Experiences",
                column: "UserModelUserId",
                principalTable: "UserModels",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_UserModels_UserModelUserId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Experiences");

            migrationBuilder.RenameColumn(
                name: "UserModelUserId",
                table: "Experiences",
                newName: "UserModel");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_UserModelUserId",
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
    }
}
