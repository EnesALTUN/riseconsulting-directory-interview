using Microsoft.EntityFrameworkCore.Migrations;

namespace RiseConsulting.Directory.Data.Migrations
{
    public partial class FiveInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DirectoryUsers_UserId",
                table: "DirectoryUsers");

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryUsers_UserId",
                table: "DirectoryUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DirectoryUsers_UserId",
                table: "DirectoryUsers");

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryUsers_UserId",
                table: "DirectoryUsers",
                column: "UserId",
                unique: true);
        }
    }
}
