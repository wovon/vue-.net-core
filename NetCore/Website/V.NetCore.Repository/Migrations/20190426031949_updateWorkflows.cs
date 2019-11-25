using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateWorkflows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWorkflow_Workflow_WorkflowId",
                table: "ProjectWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_WorkOrderManagements_WorkOrderManagementId",
                table: "Workflow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow");

            migrationBuilder.RenameTable(
                name: "Workflow",
                newName: "WorkFlows");

            migrationBuilder.RenameIndex(
                name: "IX_Workflow_WorkOrderManagementId",
                table: "WorkFlows",
                newName: "IX_WorkFlows_WorkOrderManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFlows",
                table: "WorkFlows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWorkflow_WorkFlows_WorkflowId",
                table: "ProjectWorkflow",
                column: "WorkflowId",
                principalTable: "WorkFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlows_WorkOrderManagements_WorkOrderManagementId",
                table: "WorkFlows",
                column: "WorkOrderManagementId",
                principalTable: "WorkOrderManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWorkflow_WorkFlows_WorkflowId",
                table: "ProjectWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlows_WorkOrderManagements_WorkOrderManagementId",
                table: "WorkFlows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFlows",
                table: "WorkFlows");

            migrationBuilder.RenameTable(
                name: "WorkFlows",
                newName: "Workflow");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlows_WorkOrderManagementId",
                table: "Workflow",
                newName: "IX_Workflow_WorkOrderManagementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWorkflow_Workflow_WorkflowId",
                table: "ProjectWorkflow",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_WorkOrderManagements_WorkOrderManagementId",
                table: "Workflow",
                column: "WorkOrderManagementId",
                principalTable: "WorkOrderManagements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
