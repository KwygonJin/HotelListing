#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Ukraine", "UA" },
                    { 2, "Jamaica", "JM" },
                    { 3, "Bahamas", "BS" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "Country", "CountryId", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "Lviv", null, 1, "Hotel Ukraine", 4.5 },
                    { 2, "Jamaica", null, 2, "Hotel Jamaica", 4.7999999999999998 },
                    { 3, "Bahamas", null, 3, "Hotel Bahamas", 5.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
