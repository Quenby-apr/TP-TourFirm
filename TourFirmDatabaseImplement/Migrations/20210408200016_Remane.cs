using Microsoft.EntityFrameworkCore.Migrations;

namespace TourFirmDatabaseImplement.Migrations
{
    public partial class Remane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {/*
            migrationBuilder.DropTable(
                name: "ExcursionGuides");

            migrationBuilder.CreateTable(
                name: "GuideExcursions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcursionID = table.Column<int>(nullable: false),
                    GuideID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideExcursions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GuideExcursions_Excursions_ExcursionID",
                        column: x => x.ExcursionID,
                        principalTable: "Excursions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuideExcursions_Guides_GuideID",
                        column: x => x.GuideID,
                        principalTable: "Guides",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuideExcursions_ExcursionID",
                table: "GuideExcursions",
                column: "ExcursionID");

            migrationBuilder.CreateIndex(
                name: "IX_GuideExcursions_GuideID",
                table: "GuideExcursions",
                column: "GuideID");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "GuideExcursions");

            migrationBuilder.CreateTable(
                name: "ExcursionGuides",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcursionID = table.Column<int>(type: "int", nullable: false),
                    GuideID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionGuides_ExcursionID",
                table: "ExcursionGuides",
                column: "ExcursionID");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionGuides_GuideID",
                table: "ExcursionGuides",
                column: "GuideID");*/
        }
    }
}
