using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementSystem.Migrations
{
    public partial class Initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Project");

            migrationBuilder.AddColumn<byte[]>(
                name: "Attachment",
                table: "Project",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
