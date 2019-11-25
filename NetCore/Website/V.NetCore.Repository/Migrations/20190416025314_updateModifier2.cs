using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateModifier2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "wom_Id",
                table: "Modifiers",
                newName: "WOM_Id");

            migrationBuilder.RenameColumn(
                name: "so_Id",
                table: "Modifiers",
                newName: "SO_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WOM_Id",
                table: "Modifiers",
                newName: "wom_Id");

            migrationBuilder.RenameColumn(
                name: "SO_Id",
                table: "Modifiers",
                newName: "so_Id");
        }
    }
}
