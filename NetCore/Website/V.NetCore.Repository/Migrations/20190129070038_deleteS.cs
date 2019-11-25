using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class deleteS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentRoles_Departments_DepartmentId",
                table: "DepartmentRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentRoles_Roles_RoleId",
                table: "DepartmentRoles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_DepartmentRoles_Id",
                table: "DepartmentRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentRoles",
                table: "DepartmentRoles");

            migrationBuilder.RenameTable(
                name: "DepartmentRoles",
                newName: "DepartmentRole");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentRoles_RoleId",
                table: "DepartmentRole",
                newName: "IX_DepartmentRole_RoleId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DepartmentRole_Id",
                table: "DepartmentRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentRole",
                table: "DepartmentRole",
                columns: new[] { "DepartmentId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentRole_Departments_DepartmentId",
                table: "DepartmentRole",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentRole_Roles_RoleId",
                table: "DepartmentRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentRole_Departments_DepartmentId",
                table: "DepartmentRole");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentRole_Roles_RoleId",
                table: "DepartmentRole");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_DepartmentRole_Id",
                table: "DepartmentRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentRole",
                table: "DepartmentRole");

            migrationBuilder.RenameTable(
                name: "DepartmentRole",
                newName: "DepartmentRoles");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentRole_RoleId",
                table: "DepartmentRoles",
                newName: "IX_DepartmentRoles_RoleId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DepartmentRoles_Id",
                table: "DepartmentRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentRoles",
                table: "DepartmentRoles",
                columns: new[] { "DepartmentId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentRoles_Departments_DepartmentId",
                table: "DepartmentRoles",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentRoles_Roles_RoleId",
                table: "DepartmentRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
