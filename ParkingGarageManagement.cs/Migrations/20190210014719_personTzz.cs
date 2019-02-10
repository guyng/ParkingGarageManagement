using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class personTzz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PersonId",
                table: "Vehicles",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Persons_PersonId",
                table: "Vehicles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Persons_PersonId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_PersonId",
                table: "Vehicles");
        }
    }
}
