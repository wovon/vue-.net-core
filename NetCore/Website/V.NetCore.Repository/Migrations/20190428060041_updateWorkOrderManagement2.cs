using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateWorkOrderManagement2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_WorkFlows_WorkOrderManagements_WorkOrderManagementId",
                table: "WorkFlows");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlows_WorkOrderManagementId",
                table: "WorkFlows");

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
                table: "SelectOptions");

            migrationBuilder.DropColumn(
                name: "WorkOrderManagementId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WorkOrderManagementId",
                table: "Modifiers");

            migrationBuilder.AlterColumn<string>(
                name: "WorkOrderManagementId",
                table: "WorkFlows",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkOrderManagementId",
                table: "WorkFlows",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
                name: "History",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    GU_ID = table.Column<int>(nullable: false),
                    Remake = table.Column<string>(maxLength: 50, nullable: true),
                    Updatetime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Wom_ID = table.Column<int>(nullable: false),
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
                name: "IX_WorkFlows_WorkOrderManagementId",
                table: "WorkFlows",
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
                name: "FK_WorkFlows_WorkOrderManagements_WorkOrderManagementId",
                table: "WorkFlows",
                column: "WorkOrderManagementId",
                principalTable: "WorkOrderManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
