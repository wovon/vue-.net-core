using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateRoleDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CascadeId",
                table: "Roles",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Roles",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentName",
                table: "Roles",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UseChildId",
                table: "Departments",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CascadeId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ParentName",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UseChildId",
                table: "Departments");
        }
    }
}
