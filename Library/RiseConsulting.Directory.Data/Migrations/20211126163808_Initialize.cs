using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiseConsulting.Directory.Data.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DirectoryUsers",
                columns: table => new
                {
                    DirectoryUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsersUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectoryUsers", x => x.DirectoryUsersId);
                    table.ForeignKey(
                        name: "FK_DirectoryUsers_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectoryUsers_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationInformation",
                columns: table => new
                {
                    CommunicationInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DirectoryUsersId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationInformation", x => x.CommunicationInformationId);
                    table.ForeignKey(
                        name: "FK_CommunicationInformation_DirectoryUsers_DirectoryUsersId",
                        column: x => x.DirectoryUsersId,
                        principalTable: "DirectoryUsers",
                        principalColumn: "DirectoryUsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationInformation_DirectoryUsersId",
                table: "CommunicationInformation",
                column: "DirectoryUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryUsers_CompanyId",
                table: "DirectoryUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryUsers_UsersUserId",
                table: "DirectoryUsers",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunicationInformation");

            migrationBuilder.DropTable(
                name: "DirectoryUsers");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
