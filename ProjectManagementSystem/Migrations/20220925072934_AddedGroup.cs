using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementSystem.Migrations
{
    public partial class AddedGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Supervisor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "SupervisorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_GroupId",
                table: "Student",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_GroupId",
                table: "Project",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_SupervisorId",
                table: "Group",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Group_GroupId",
                table: "Project",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Group_GroupId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Student_GroupId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Project_GroupId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Project");
        }
    }
}
