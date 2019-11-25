using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CascadeId",
                table: "MenuButton");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MenuButton");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "MenuButton");

            migrationBuilder.DropColumn(
                name: "ParentName",
                table: "MenuButton");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CascadeId",
                table: "MenuButton",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MenuButton",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "MenuButton",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentName",
                table: "MenuButton",
                maxLength: 50,
                nullable: true);
        }
    }
}
