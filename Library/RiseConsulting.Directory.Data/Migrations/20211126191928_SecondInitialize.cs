using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiseConsulting.Directory.Data.Migrations
{
    public partial class SecondInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectoryUsers_Users_UsersUserId",
                table: "DirectoryUsers");

            migrationBuilder.DropIndex(
                name: "IX_DirectoryUsers_UsersUserId",
                table: "DirectoryUsers");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "DirectoryUsers");

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryUsers_UserId",
                table: "DirectoryUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectoryUsers_Users_UserId",
                table: "DirectoryUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectoryUsers_Users_UserId",
                table: "DirectoryUsers");

            migrationBuilder.DropIndex(
                name: "IX_DirectoryUsers_UserId",
                table: "DirectoryUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersUserId",
                table: "DirectoryUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryUsers_UsersUserId",
                table: "DirectoryUsers",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectoryUsers_Users_UsersUserId",
                table: "DirectoryUsers",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
