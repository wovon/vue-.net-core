using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateRoleMenuButton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ButtonId",
                table: "RoleMenuButton",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuId",
                table: "RoleMenuButton",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "RoleMenuButton",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonId",
                table: "RoleMenuButton");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "RoleMenuButton");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "RoleMenuButton");
        }
    }
}
