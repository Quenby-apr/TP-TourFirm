using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourFirmDatabaseImplement.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Mail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tourists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Mail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    WorkPlace = table.Column<string>(nullable: false),
                    MainLanguage = table.Column<string>(nullable: false),
                    AdditionalLanguage = table.Column<string>(nullable: true),
                    DateWork = table.Column<DateTime>(nullable: false),
                    OperatorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Guides_Operators_OperatorID",
                        column: x => x.OperatorID,
                        principalTable: "Operators",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Halts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    OperatorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Halts_Operators_OperatorID",
                        column: x => x.OperatorID,
                        principalTable: "Operators",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    TouristID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Places_Tourists_TouristID",
                        column: x => x.TouristID,
                        principalTable: "Tourists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    TouristID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Travels_Tourists_TouristID",
                        column: x => x.TouristID,
                        principalTable: "Tourists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    OperatorID = table.Column<int>(nullable: false),
                    HaltID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tours_Halts_HaltID",
                        column: x => x.HaltID,
                        principalTable: "Halts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_Operators_OperatorID",
                        column: x => x.OperatorID,
                        principalTable: "Operators",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    PlaceID = table.Column<int>(nullable: false),
                    TouristID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Excursions_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_Tourists_TouristID",
                        column: x => x.TouristID,
                        principalTable: "Tourists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TourGuides",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourID = table.Column<int>(nullable: false),
                    GuideID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TourGuides_Guides_GuideID",
                        column: x => x.GuideID,
                        principalTable: "Guides",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourGuides_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelTours",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelID = table.Column<int>(nullable: false),
                    TourID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelTours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TravelTours_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelTours_Travels_TravelID",
                        column: x => x.TravelID,
                        principalTable: "Travels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExcursionGuides",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcursionID = table.Column<int>(nullable: false),
                    GuideID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionGuides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExcursionGuides_Excursions_ExcursionID",
                        column: x => x.ExcursionID,
                        principalTable: "Excursions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcursionGuides_Guides_GuideID",
                        column: x => x.GuideID,
                        principalTable: "Guides",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelExcursions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelID = table.Column<int>(nullable: false),
                    ExcursionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelExcursions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TravelExcursions_Excursions_ExcursionID",
                        column: x => x.ExcursionID,
                        principalTable: "Excursions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelExcursions_Travels_TravelID",
                        column: x => x.TravelID,
                        principalTable: "Travels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionGuides_ExcursionID",
                table: "ExcursionGuides",
                column: "ExcursionID");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionGuides_GuideID",
                table: "ExcursionGuides",
                column: "GuideID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_PlaceID",
                table: "Excursions",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_TouristID",
                table: "Excursions",
                column: "TouristID");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_OperatorID",
                table: "Guides",
                column: "OperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Halts_OperatorID",
                table: "Halts",
                column: "OperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TouristID",
                table: "Places",
                column: "TouristID");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_GuideID",
                table: "TourGuides",
                column: "GuideID");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_TourID",
                table: "TourGuides",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_HaltID",
                table: "Tours",
                column: "HaltID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_OperatorID",
                table: "Tours",
                column: "OperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelExcursions_ExcursionID",
                table: "TravelExcursions",
                column: "ExcursionID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelExcursions_TravelID",
                table: "TravelExcursions",
                column: "TravelID");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_TouristID",
                table: "Travels",
                column: "TouristID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelTours_TourID",
                table: "TravelTours",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelTours_TravelID",
                table: "TravelTours",
                column: "TravelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcursionGuides");

            migrationBuilder.DropTable(
                name: "TourGuides");

            migrationBuilder.DropTable(
                name: "TravelExcursions");

            migrationBuilder.DropTable(
                name: "TravelTours");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Halts");

            migrationBuilder.DropTable(
                name: "Tourists");

            migrationBuilder.DropTable(
                name: "Operators");
        }
    }
}
