using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class RepurchaseAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "levelCount",
                table: "LevelWiseMember");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Members",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberType",
                table: "Members",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reward",
                table: "LevelWiseMember",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberType",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Reward",
                table: "LevelWiseMember");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Members",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "levelCount",
                table: "LevelWiseMember",
                type: "int",
                nullable: true);
        }
    }
}
