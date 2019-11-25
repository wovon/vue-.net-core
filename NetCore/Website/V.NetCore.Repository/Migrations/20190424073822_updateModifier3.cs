using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateModifier3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Workflow",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Modifiers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<string>(maxLength: 4000, nullable: true),
                    ParentName = table.Column<string>(maxLength: 4000, nullable: true),
                    CascadeId = table.Column<string>(maxLength: 100, nullable: true),
                    Label = table.Column<string>(maxLength: 50, nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    Explain = table.Column<string>(maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsAble = table.Column<bool>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectWorkflow",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    ProjectId = table.Column<string>(nullable: false),
                    WorkflowId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWorkflow", x => new { x.ProjectId, x.WorkflowId });
                    table.UniqueConstraint("AK_ProjectWorkflow_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWorkflow_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectWorkflow_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorkflow_WorkflowId",
                table: "ProjectWorkflow",
                column: "WorkflowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectWorkflow");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Modifiers");
        }
    }
}
