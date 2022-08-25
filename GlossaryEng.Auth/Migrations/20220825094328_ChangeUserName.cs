using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlossaryEng.Auth.Migrations
{
    public partial class ChangeUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RefreshTokens",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_IdentityUserId",
                table: "RefreshTokens",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_IdentityUserId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "RefreshTokens",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_IdentityUserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
