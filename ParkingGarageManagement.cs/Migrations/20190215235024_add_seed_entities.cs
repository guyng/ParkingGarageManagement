using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class add_seed_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "A" },
                    { 2, "B" },
                    { 3, "C" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Name", "PersonTz", "Phone" },
                values: new object[,]
                {
                    { 1, "Guy", "302119282", "0504029472" },
                    { 2, "Duy", "304119282", "0504529472" },
                    { 3, "Ruy", "307119282", "0504929472" }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "ClassId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Motorcycle" },
                    { 2, 1, "Private" },
                    { 3, 1, "Crossover" },
                    { 4, 2, "SUV" },
                    { 5, 2, "Van" },
                    { 6, 3, "Truck" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "PersonId", "TicketId", "VehicleHeight", "VehicleLength", "VehicleTypeId", "VehicleWidth" },
                values: new object[] { 1, 1, 1, 1800, 1200, 1, 1500 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "PersonId", "TicketId", "VehicleHeight", "VehicleLength", "VehicleTypeId", "VehicleWidth" },
                values: new object[] { 2, 1, 2, 2200, 1200, 2, 1500 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "PersonId", "TicketId", "VehicleHeight", "VehicleLength", "VehicleTypeId", "VehicleWidth" },
                values: new object[] { 3, 1, 3, 2600, 1200, 3, 1500 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
