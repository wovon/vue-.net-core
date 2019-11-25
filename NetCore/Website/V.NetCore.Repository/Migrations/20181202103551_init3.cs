using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MenuButton_Id",
                table: "MenuButton");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MenuButton");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "MenuButton",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MenuButton_Id",
                table: "MenuButton",
                column: "Id");
        }
    }
}
