using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateDeartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Menus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Departments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OutBox",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    username = table.Column<string>(nullable: true),
                    Mbno = table.Column<string>(nullable: true),
                    Msg = table.Column<string>(maxLength: 200, nullable: true),
                    SendTime = table.Column<DateTime>(nullable: false),
                    ComPort = table.Column<int>(nullable: false),
                    Report = table.Column<int>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutBox", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutBox");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Departments");
        }
    }
}
