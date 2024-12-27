using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class TotalModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "LevelCount",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "LevelCount",
                newName: "level");

            migrationBuilder.AlterColumn<int>(
                name: "total",
                table: "LevelCount",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "level",
                table: "LevelCount",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "member",
                table: "LevelCount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "purchase",
                table: "LevelCount",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "member",
                table: "LevelCount");

            migrationBuilder.DropColumn(
                name: "purchase",
                table: "LevelCount");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "LevelCount",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "LevelCount",
                newName: "Level");

            migrationBuilder.AlterColumn<int>(
                name: "Total",
                table: "LevelCount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "LevelCount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
