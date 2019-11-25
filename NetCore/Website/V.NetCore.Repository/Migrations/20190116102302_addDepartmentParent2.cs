using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class addDepartmentParent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentParent");
        }
    }
}
