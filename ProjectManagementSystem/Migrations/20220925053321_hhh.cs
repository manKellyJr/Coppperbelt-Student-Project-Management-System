using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementSystem.Migrations
{
    public partial class hhh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinator_Student_StudentId",
                table: "Coordinator");

            migrationBuilder.DropIndex(
                name: "IX_Coordinator_StudentId",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Coordinator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Coordinator",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinator_StudentId",
                table: "Coordinator",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinator_Student_StudentId",
                table: "Coordinator",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId");
        }
    }
}
