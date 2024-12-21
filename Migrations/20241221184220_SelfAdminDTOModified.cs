using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class SelfAdminDTOModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total",
                table: "AllSelfMember");

            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "AllSelfMember",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefId",
                table: "AllSelfMember");

            migrationBuilder.AddColumn<int>(
                name: "total",
                table: "AllSelfMember",
                type: "int",
                nullable: true);
        }
    }
}
