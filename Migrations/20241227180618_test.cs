using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "total",
                table: "AllSelfMember",
                newName: "pTotal");

            migrationBuilder.AddColumn<int>(
                name: "mTotal",
                table: "AllSelfMember",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mTotal",
                table: "AllSelfMember");

            migrationBuilder.RenameColumn(
                name: "pTotal",
                table: "AllSelfMember",
                newName: "total");
        }
    }
}
