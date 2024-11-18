using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class levelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reward",
                table: "LevelWiseMember");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reward",
                table: "LevelWiseMember",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
