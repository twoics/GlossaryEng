using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlossaryEng.Auth.Migrations
{
    public partial class RenameUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_IdentityUserId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "RefreshTokens",
                newName: "UserDbId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_IdentityUserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserDbId",
                table: "RefreshTokens",
                column: "UserDbId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserDbId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UserDbId",
                table: "RefreshTokens",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserDbId",
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
    }
}
