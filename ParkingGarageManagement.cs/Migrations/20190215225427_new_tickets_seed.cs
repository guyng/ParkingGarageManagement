using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class new_tickets_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Cost", "MaxHeight", "MaxLength", "MaxWidth", "Name", "TicketTypeId", "TimeLimit" },
                values: new object[] { 1, 200, -1, -1, -1, "Vip", 1, 72 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Cost", "MaxHeight", "MaxLength", "MaxWidth", "Name", "TicketTypeId", "TimeLimit" },
                values: new object[] { 2, 200, -1, -1, -1, "Value", 2, 72 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Cost", "MaxHeight", "MaxLength", "MaxWidth", "Name", "TicketTypeId", "TimeLimit" },
                values: new object[] { 3, 200, -1, -1, -1, "Regular", 3, 72 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Cost", "MaxHeight", "MaxLength", "MaxWidth", "Name", "TicketTypeId", "TimeLimit" },
                values: new object[] { -1, 200, -1, -1, -1, "Vip", 1, 72 });
        }
    }
}
