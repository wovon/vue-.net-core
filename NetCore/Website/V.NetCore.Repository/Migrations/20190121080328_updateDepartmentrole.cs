using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateDepartmentrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "DepartmentRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "DepartmentRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "DepartmentRoles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "DepartmentRoles");
        }
    }
}
