using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateDepartmentAndDepartmentParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentParent");

            migrationBuilder.DropColumn(
                name: "UseChildId",
                table: "Departments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UseChildId",
                table: "Departments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DepartmentParent",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    DepartmentId = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentParent", x => x.Id);
                });
        }
    }
}
