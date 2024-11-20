using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class BannerData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BannerMaster",
                columns: new[] { "AutoId", "Banner" },
                values: new object[,]
                {
                    { 1L, "slider1.png" },
                    { 2L, "slider2.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BannerMaster",
                keyColumn: "AutoId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BannerMaster",
                keyColumn: "AutoId",
                keyValue: 2L);
        }
    }
}
