using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "LotPosition" },
                values: new object[] { new DateTime(2019, 2, 27, 15, 43, 24, 301, DateTimeKind.Local).AddTicks(5374), 1 });

            migrationBuilder.UpdateData(
                table: "Lots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckIn", "LotPosition" },
                values: new object[] { new DateTime(2019, 2, 28, 15, 43, 24, 303, DateTimeKind.Local).AddTicks(564), 11 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "LotPosition" },
                values: new object[] { new DateTime(2019, 2, 27, 15, 41, 52, 533, DateTimeKind.Local).AddTicks(9109), 0 });

            migrationBuilder.UpdateData(
                table: "Lots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckIn", "LotPosition" },
                values: new object[] { new DateTime(2019, 2, 28, 15, 41, 52, 535, DateTimeKind.Local).AddTicks(4928), 0 });
        }
    }
}
