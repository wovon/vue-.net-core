using Microsoft.EntityFrameworkCore.Migrations;

namespace V.NetCore.Repository.Migrations
{
    public partial class updateUserDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "UserDepartment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserDepartment",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserDepartment",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserDepartment_Id",
                table: "UserDepartment",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserDepartment_Id",
                table: "UserDepartment");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "UserDepartment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserDepartment");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserDepartment");
        }
    }
}
