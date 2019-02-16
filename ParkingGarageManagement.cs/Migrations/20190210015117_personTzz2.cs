using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingGarageManagement.cs.Migrations
{
    public partial class personTzz2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	//        migrationBuilder.RenameColumn("PersonId", "Persons", "PersonTz");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
