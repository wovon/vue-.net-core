using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "DepartmentRoles",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DepartmentRoles_Id",
                table: "DepartmentRoles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_DepartmentRoles_Id",
                table: "DepartmentRoles");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DepartmentRoles");
        }
    }
}
