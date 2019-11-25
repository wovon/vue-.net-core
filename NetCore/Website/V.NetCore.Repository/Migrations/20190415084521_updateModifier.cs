using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateModifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WOM_ID",
                table: "Modifiers",
                newName: "wom_Id");

            migrationBuilder.RenameColumn(
                name: "User_ID",
                table: "Modifiers",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "SO_ID",
                table: "Modifiers",
                newName: "so_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "wom_Id",
                table: "Modifiers",
                newName: "WOM_ID");

            migrationBuilder.RenameColumn(
                name: "so_Id",
                table: "Modifiers",
                newName: "SO_ID");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Modifiers",
                newName: "User_ID");
        }
    }
}
