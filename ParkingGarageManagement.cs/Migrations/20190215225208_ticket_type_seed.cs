using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class ticket_type_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, 0 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Cost", "MaxHeight", "MaxLength", "MaxWidth", "Name", "TicketTypeId", "TimeLimit" },
                values: new object[] { -1, 200, -1, -1, -1, "Vip", 1, 72 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
