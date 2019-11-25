using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class addSelectOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectOptions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<string>(maxLength: 4000, nullable: true),
                    ParentName = table.Column<string>(maxLength: 4000, nullable: true),
                    CascadeId = table.Column<string>(maxLength: 100, nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    IsHide = table.Column<bool>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectOptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectOptions");
        }
    }
}
