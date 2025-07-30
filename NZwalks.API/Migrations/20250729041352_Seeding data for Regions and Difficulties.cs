using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZwalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforRegionsandDifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4040018d-1aeb-4179-99ac-2232120ba15a"), "Hard" },
                    { new Guid("d8161b72-6a3a-4258-ac84-6c3d0c049516"), "Easy" },
                    { new Guid("fe04b1d3-89c6-4717-ae27-791758675f1d"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("ab7721d4-cc6a-4617-aafb-bc20d77961b7"), "AKL", "Auckland", "https://example.com/north-island.jpg" },
                    { new Guid("bd89c763-63de-4e95-a7ca-2db723007d95"), "STL", "Southland", "https://example.com/south-island.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4040018d-1aeb-4179-99ac-2232120ba15a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d8161b72-6a3a-4258-ac84-6c3d0c049516"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("fe04b1d3-89c6-4717-ae27-791758675f1d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ab7721d4-cc6a-4617-aafb-bc20d77961b7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("bd89c763-63de-4e95-a7ca-2db723007d95"));
        }
    }
}
