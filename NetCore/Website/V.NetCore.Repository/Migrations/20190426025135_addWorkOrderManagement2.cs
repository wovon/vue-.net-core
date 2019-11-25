using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class addWorkOrderManagement2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkOrderManagementId",
                table: "Workflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderManagementId",
                table: "SelectOptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderManagementId",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderManagementId",
                table: "Modifiers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkOrderManagements",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    FeedbackTime = table.Column<DateTime>(nullable: false),
                    FeedbackUserId = table.Column<string>(nullable: true),
                    FeedbackUserName = table.Column<string>(maxLength: 500, nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(maxLength: 500, nullable: true),
                    UserName = table.Column<string>(maxLength: 500, nullable: true),
                    DepartmentId = table.Column<string>(maxLength: 500, nullable: true),
                    DepartmentName = table.Column<string>(maxLength: 500, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Origin = table.Column<string>(maxLength: 500, nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderManagements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    GU_ID = table.Column<int>(nullable: false),
                    Wom_ID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Updatetime = table.Column<DateTime>(nullable: false),
                    Remake = table.Column<string>(maxLength: 50, nullable: true),
                    WorkOrderManagementId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_WorkOrderManagements_WorkOrderManagementId",
                        column: x => x.WorkOrderManagementId,
                        principalTable: "WorkOrderManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_WorkOrderManagementId",
                table: "Workflow",
                column: "WorkOrderManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectOptions_WorkOrderManagementId",
                table: "SelectOptions",
                column: "WorkOrderManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_WorkOrderManagementId",
                table: "Project",
                column: "WorkOrderManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_WorkOrderManagementId",
                table: "Modifiers",
                column: "WorkOrderManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_History_WorkOrderManagementId",
                table: "History",
                column: "WorkOrderManagementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modifiers_WorkOrderManagements_WorkOrderManagementId",
                table: "Modifiers",
                column: "WorkOrderManagementId",
                principalTable: "WorkOrderManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_WorkOrderManagements_WorkOrderManagementId",
                table: "Project",
                column: "WorkOrderManagementId",
                principalTable: "WorkOrderManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectOptions_WorkOrderManagements_WorkOrderManagementId",
                table: "SelectOptions",
                column: "WorkOrderManagementId",
                principalTable: "WorkOrderManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_WorkOrderManagements_WorkOrderManagementId",
                table: "Workflow",
                column: "WorkOrderManagementId",
                principalTable: "WorkOrderManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modifiers_WorkOrderManagements_WorkOrderManagementId",
                table: "Modifiers");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_WorkOrderManagements_WorkOrderManagementId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectOptions_WorkOrderManagements_WorkOrderManagementId",
                table: "SelectOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_WorkOrderManagements_WorkOrderManagementId",
                table: "Workflow");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "WorkOrderManagements");

            migrationBuilder.DropIndex(
                name: "IX_Workflow_WorkOrderManagementId",
                table: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_SelectOptions_WorkOrderManagementId",
                table: "SelectOptions");

            migrationBuilder.DropIndex(
                name: "IX_Project_WorkOrderManagementId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Modifiers_WorkOrderManagementId",
                table: "Modifiers");

            migrationBuilder.DropColumn(
                name: "WorkOrderManagementId",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "WorkOrderManagementId",
                table: "SelectOptions");

            migrationBuilder.DropColumn(
                name: "WorkOrderManagementId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WorkOrderManagementId",
                table: "Modifiers");
        }
    }
}
