using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarFleet.Data.Migrations
{
    public partial class CarBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carBrands",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    brandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carBrands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    launchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    releaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fuelType = table.Column<int>(type: "int", nullable: false),
                    transmissionType = table.Column<int>(type: "int", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    brandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.id);
                    table.ForeignKey(
                        name: "FK_cars_carBrands_brandId",
                        column: x => x.brandId,
                        principalTable: "carBrands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_brandId",
                table: "cars",
                column: "brandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "carBrands");
        }
    }
}