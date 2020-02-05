using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class int3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Tasks",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Tasks");
        }
    }
}
