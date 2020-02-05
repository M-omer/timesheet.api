using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class inti2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "TimeSheets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_EmployeeID",
                table: "TimeSheets",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheets_Employees_EmployeeID",
                table: "TimeSheets",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheets_Employees_EmployeeID",
                table: "TimeSheets");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheets_EmployeeID",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "TimeSheets");
        }
    }
}
