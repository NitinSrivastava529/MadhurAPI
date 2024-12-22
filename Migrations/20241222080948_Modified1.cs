using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class Modified1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefId",
                table: "AllSelfMember");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "AllMember",
                newName: "City");

            migrationBuilder.AddColumn<int>(
                name: "total",
                table: "AllSelfMember",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total",
                table: "AllSelfMember");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "AllMember",
                newName: "city");

            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "AllSelfMember",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
