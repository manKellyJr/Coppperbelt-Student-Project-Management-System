using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementSystem.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectAssessment",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntellectualQuality = table.Column<int>(type: "int", nullable: false),
                    KnowledgeOfResearch = table.Column<int>(type: "int", nullable: false),
                    QualityOfPresentation = table.Column<int>(type: "int", nullable: false),
                    ResponseToQuestions = table.Column<int>(type: "int", nullable: false),
                    StructureOfPresentation = table.Column<int>(type: "int", nullable: false),
                    ReferencingAndAppendices = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssessment", x => new { x.ProjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProjectAssessment_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAssessment");
        }
    }
}
