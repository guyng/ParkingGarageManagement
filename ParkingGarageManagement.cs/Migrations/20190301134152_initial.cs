using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PersonTz = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketTypeId = table.Column<int>(nullable: false),
                    TicketType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MaxHeight = table.Column<int>(nullable: false),
                    MaxWidth = table.Column<int>(nullable: false),
                    MaxLength = table.Column<int>(nullable: false),
                    TimeLimit = table.Column<int>(nullable: false),
                    Cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypes_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotRanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(nullable: false),
                    MinRange = table.Column<int>(nullable: false),
                    MaxRange = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotRanges_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<int>(nullable: false),
                    VehicleTypeId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    VehicleHeight = table.Column<int>(nullable: false),
                    VehicleWidth = table.Column<int>(nullable: false),
                    VehicleLength = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LotPosition = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false),
                    CheckIn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Tickets",
                columns: new[] { "Id", "Cost", "MaxHeight", "MaxLength", "MaxWidth", "Name", "TicketType", "TicketTypeId", "TimeLimit" },
                values: new object[,]
                {
                    { 1, 200, -1, -1, -1, "Vip", 2, 0, 72 },
                    { 2, 100, 2500, 2500, 2500, "Value", 1, 0, 48 },
                    { 3, 50, 2000, 2000, 2000, "Regular", 0, 0, 24 }
                });

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
                values: new object[] { 1, 1, 3, 1800, 1200, 1, 1500 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "PersonId", "TicketId", "VehicleHeight", "VehicleLength", "VehicleTypeId", "VehicleWidth" },
                values: new object[] { 2, 1, 2, 2200, 1200, 2, 1500 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "PersonId", "TicketId", "VehicleHeight", "VehicleLength", "VehicleTypeId", "VehicleWidth" },
                values: new object[] { 3, 1, 1, 2600, 1200, 3, 1500 });

            migrationBuilder.InsertData(
                table: "Lots",
                columns: new[] { "Id", "CheckIn", "LotPosition", "VehicleId" },
                values: new object[] { 1, new DateTime(2019, 2, 27, 15, 41, 52, 533, DateTimeKind.Local).AddTicks(9109), 0, 1 });

            migrationBuilder.InsertData(
                table: "Lots",
                columns: new[] { "Id", "CheckIn", "LotPosition", "VehicleId" },
                values: new object[] { 2, new DateTime(2019, 2, 28, 15, 41, 52, 535, DateTimeKind.Local).AddTicks(4928), 0, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_LotRanges_TicketId",
                table: "LotRanges",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_VehicleId",
                table: "Lots",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonTz",
                table: "Persons",
                column: "PersonTz",
                unique: true,
                filter: "[PersonTz] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TicketId",
                table: "Vehicles",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_ClassId",
                table: "VehicleTypes",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotRanges");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "TicketClasses");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
