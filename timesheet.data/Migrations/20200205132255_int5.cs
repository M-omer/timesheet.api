using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class int5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AWEHours",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TWEHours",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AWEHours",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TWEHours",
                table: "Employees");
        }
    }
}
