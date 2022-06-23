using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.Migrations
{
    public partial class Addedroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12101189-7575-49e1-8352-b1ca41e8a246", "11c1a626-914e-46f5-956b-5cd5589c485e", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12eb8f48-e687-4a5e-84cf-06b2847475ad", "15f0cf0c-4472-48ef-a617-1a768792ff89", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12101189-7575-49e1-8352-b1ca41e8a246");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12eb8f48-e687-4a5e-84cf-06b2847475ad");
        }
    }
}
