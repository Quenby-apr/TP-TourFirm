using Microsoft.EntityFrameworkCore.Migrations;

namespace TourFirmDatabaseImplement.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Tourists_TouristID",
                table: "Excursions");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Tourists_TouristID",
                table: "Excursions",
                column: "TouristID",
                principalTable: "Tourists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Tourists_TouristID",
                table: "Excursions");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Tourists_TouristID",
                table: "Excursions",
                column: "TouristID",
                principalTable: "Tourists",
                principalColumn: "ID");
        }
    }
}
