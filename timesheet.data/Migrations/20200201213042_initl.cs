using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class initl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tasks",
                newName: "TaskID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "EmployeeID");

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "TaskEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    TaskID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEmployees", x => new { x.TaskID, x.EmployeeID });
                    table.UniqueConstraint("AK_TaskEmployees_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    TimesheetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<string>(nullable: true),
                    EndDate = table.Column<string>(nullable: true),
                    Hours = table.Column<string>(nullable: true),
                    DayId = table.Column<int>(nullable: true),
                    TaskID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.TimesheetID);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_EmployeeID",
                table: "TaskEmployees",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_DayId",
                table: "TimeSheets",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_TaskID",
                table: "TimeSheets",
                column: "TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskEmployees");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.RenameColumn(
                name: "TaskID",
                table: "Tasks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "Employees",
                newName: "Id");
        }
    }
}
