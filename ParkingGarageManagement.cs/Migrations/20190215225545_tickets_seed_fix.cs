using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class tickets_seed_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cost", "MaxHeight", "MaxLength", "MaxWidth", "TimeLimit" },
                values: new object[] { 100, 2500, 2500, 2500, 48 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Cost", "MaxHeight", "MaxLength", "MaxWidth", "TimeLimit" },
                values: new object[] { 50, 2000, 2000, 2000, 24 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cost", "MaxHeight", "MaxLength", "MaxWidth", "TimeLimit" },
                values: new object[] { 200, -1, -1, -1, 72 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Cost", "MaxHeight", "MaxLength", "MaxWidth", "TimeLimit" },
                values: new object[] { 200, -1, -1, -1, 72 });
        }
    }
}
