using Microsoft.EntityFrameworkCore.Migrations;

namespace TourFirmDatabaseImplement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Place_PlaceID",
                table: "Excursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Tourist_TouristID",
                table: "Excursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Place_Tourist_TouristID",
                table: "Place");

            migrationBuilder.DropForeignKey(
                name: "FK_Travel_Tourist_TouristID",
                table: "Travel");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelExcursion_Excursions_ExcursionID",
                table: "TravelExcursion");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelExcursion_Travel_TravelID",
                table: "TravelExcursion");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelTour_Tours_TourID",
                table: "TravelTour");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelTour_Travel_TravelID",
                table: "TravelTour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelTour",
                table: "TravelTour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelExcursion",
                table: "TravelExcursion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Travel",
                table: "Travel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tourist",
                table: "Tourist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.RenameTable(
                name: "TravelTour",
                newName: "TravelTours");

            migrationBuilder.RenameTable(
                name: "TravelExcursion",
                newName: "TravelExcursions");

            migrationBuilder.RenameTable(
                name: "Travel",
                newName: "Travels");

            migrationBuilder.RenameTable(
                name: "Tourist",
                newName: "Tourists");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.RenameIndex(
                name: "IX_TravelTour_TravelID",
                table: "TravelTours",
                newName: "IX_TravelTours_TravelID");

            migrationBuilder.RenameIndex(
                name: "IX_TravelTour_TourID",
                table: "TravelTours",
                newName: "IX_TravelTours_TourID");

            migrationBuilder.RenameIndex(
                name: "IX_TravelExcursion_TravelID",
                table: "TravelExcursions",
                newName: "IX_TravelExcursions_TravelID");

            migrationBuilder.RenameIndex(
                name: "IX_TravelExcursion_ExcursionID",
                table: "TravelExcursions",
                newName: "IX_TravelExcursions_ExcursionID");

            migrationBuilder.RenameIndex(
                name: "IX_Travel_TouristID",
                table: "Travels",
                newName: "IX_Travels_TouristID");

            migrationBuilder.RenameIndex(
                name: "IX_Place_TouristID",
                table: "Places",
                newName: "IX_Places_TouristID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelTours",
                table: "TravelTours",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelExcursions",
                table: "TravelExcursions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Travels",
                table: "Travels",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tourists",
                table: "Tourists",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Places_PlaceID",
                table: "Excursions",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Tourists_TouristID",
                table: "Excursions",
                column: "TouristID",
                principalTable: "Tourists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Tourists_TouristID",
                table: "Places",
                column: "TouristID",
                principalTable: "Tourists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelExcursions_Excursions_ExcursionID",
                table: "TravelExcursions",
                column: "ExcursionID",
                principalTable: "Excursions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelExcursions_Travels_TravelID",
                table: "TravelExcursions",
                column: "TravelID",
                principalTable: "Travels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Tourists_TouristID",
                table: "Travels",
                column: "TouristID",
                principalTable: "Tourists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelTours_Tours_TourID",
                table: "TravelTours",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelTours_Travels_TravelID",
                table: "TravelTours",
                column: "TravelID",
                principalTable: "Travels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Places_PlaceID",
                table: "Excursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Tourists_TouristID",
                table: "Excursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Tourists_TouristID",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelExcursions_Excursions_ExcursionID",
                table: "TravelExcursions");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelExcursions_Travels_TravelID",
                table: "TravelExcursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Tourists_TouristID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelTours_Tours_TourID",
                table: "TravelTours");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelTours_Travels_TravelID",
                table: "TravelTours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelTours",
                table: "TravelTours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Travels",
                table: "Travels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelExcursions",
                table: "TravelExcursions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tourists",
                table: "Tourists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.RenameTable(
                name: "TravelTours",
                newName: "TravelTour");

            migrationBuilder.RenameTable(
                name: "Travels",
                newName: "Travel");

            migrationBuilder.RenameTable(
                name: "TravelExcursions",
                newName: "TravelExcursion");

            migrationBuilder.RenameTable(
                name: "Tourists",
                newName: "Tourist");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.RenameIndex(
                name: "IX_TravelTours_TravelID",
                table: "TravelTour",
                newName: "IX_TravelTour_TravelID");

            migrationBuilder.RenameIndex(
                name: "IX_TravelTours_TourID",
                table: "TravelTour",
                newName: "IX_TravelTour_TourID");

            migrationBuilder.RenameIndex(
                name: "IX_Travels_TouristID",
                table: "Travel",
                newName: "IX_Travel_TouristID");

            migrationBuilder.RenameIndex(
                name: "IX_TravelExcursions_TravelID",
                table: "TravelExcursion",
                newName: "IX_TravelExcursion_TravelID");

            migrationBuilder.RenameIndex(
                name: "IX_TravelExcursions_ExcursionID",
                table: "TravelExcursion",
                newName: "IX_TravelExcursion_ExcursionID");

            migrationBuilder.RenameIndex(
                name: "IX_Places_TouristID",
                table: "Place",
                newName: "IX_Place_TouristID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelTour",
                table: "TravelTour",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Travel",
                table: "Travel",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelExcursion",
                table: "TravelExcursion",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tourist",
                table: "Tourist",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Place_PlaceID",
                table: "Excursions",
                column: "PlaceID",
                principalTable: "Place",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Tourist_TouristID",
                table: "Excursions",
                column: "TouristID",
                principalTable: "Tourist",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Place_Tourist_TouristID",
                table: "Place",
                column: "TouristID",
                principalTable: "Tourist",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Travel_Tourist_TouristID",
                table: "Travel",
                column: "TouristID",
                principalTable: "Tourist",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelExcursion_Excursions_ExcursionID",
                table: "TravelExcursion",
                column: "ExcursionID",
                principalTable: "Excursions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelExcursion_Travel_TravelID",
                table: "TravelExcursion",
                column: "TravelID",
                principalTable: "Travel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelTour_Tours_TourID",
                table: "TravelTour",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelTour_Travel_TravelID",
                table: "TravelTour",
                column: "TravelID",
                principalTable: "Travel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
