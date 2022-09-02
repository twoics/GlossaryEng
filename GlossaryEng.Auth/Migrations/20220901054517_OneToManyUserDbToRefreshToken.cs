using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlossaryEng.Auth.Migrations
{
    public partial class OneToManyUserDbToRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserDbId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserDbId",
                table: "RefreshTokens",
                column: "UserDbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserDbId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserDbId",
                table: "RefreshTokens",
                column: "UserDbId",
                unique: true);
        }
    }
}
