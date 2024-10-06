using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadhurAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    AutoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegPin = table.Column<string>(type: "varchar(10)", nullable: true),
                    RefId = table.Column<string>(type: "varchar(10)", nullable: true),
                    MemberId = table.Column<string>(type: "varchar(10)", nullable: true),
                    MemberName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(10)", nullable: true),
                    dob = table.Column<DateTime>(type: "date", nullable: true),
                    AadharNo = table.Column<string>(type: "varchar(14)", nullable: true),
                    Address = table.Column<string>(type: "varchar(400)", nullable: true),
                    State = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    PinCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    Nominee = table.Column<string>(type: "varchar(50)", nullable: true),
                    RelationWithNominee = table.Column<string>(type: "varchar(20)", nullable: true),
                    IsActive = table.Column<string>(type: "char(1)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.AutoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
