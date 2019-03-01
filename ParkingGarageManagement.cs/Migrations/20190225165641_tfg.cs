using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class tfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LotRanges",
                columns: new[] { "Id", "MaxRange", "MinRange", "TicketId" },
                values: new object[,]
                {
                    { 1, 10, 1, 1 },
                    { 2, 30, 11, 2 },
                    { 3, 60, 31, 3 }
                });

            migrationBuilder.InsertData(
                table: "Lots",
                columns: new[] { "Id", "CheckIn", "LotPosition", "VehicleId" },
                values: new object[] { 1, new DateTime(2019, 2, 23, 18, 56, 41, 233, DateTimeKind.Local).AddTicks(3660), 0, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LotRanges",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LotRanges",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LotRanges",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lots",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "TicketId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "TicketId",
                value: 3);
        }
    }
}
