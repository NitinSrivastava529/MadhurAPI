using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class Banner2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannerMaster",
                columns: table => new
                {
                    AutoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Banner = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerMaster", x => x.AutoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerMaster");
        }
    }
}
