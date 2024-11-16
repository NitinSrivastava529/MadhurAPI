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
                name: "AllMember",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AllSelfMember",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DistrictMaster",
                columns: table => new
                {
                    recid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    state_code = table.Column<int>(type: "int", nullable: false),
                    dist_code = table.Column<int>(type: "int", nullable: false),
                    distt_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictMaster", x => x.recid);
                });

            migrationBuilder.CreateTable(
                name: "LevelCount",
                columns: table => new
                {
                    Level = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LevelReport",
                columns: table => new
                {
                    level = table.Column<int>(type: "int", nullable: false),
                    totalCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LevelWiseMember",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    levelCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

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

            migrationBuilder.CreateTable(
                name: "RegKeys",
                columns: table => new
                {
                    AuotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IsCopy = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegKeys", x => x.AuotId);
                });

            migrationBuilder.CreateTable(
                name: "RewardMaster",
                columns: table => new
                {
                    AutoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<string>(type: "varchar(20)", nullable: true),
                    level = table.Column<string>(type: "varchar(5)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    file_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardMaster", x => x.AutoId);
                });

            migrationBuilder.CreateTable(
                name: "RewardMasterDTO",
                columns: table => new
                {
                    AutoId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "StateMaster",
                columns: table => new
                {
                    RecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_id = table.Column<int>(type: "int", nullable: false),
                    state_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateMaster", x => x.RecId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllMember");

            migrationBuilder.DropTable(
                name: "AllSelfMember");

            migrationBuilder.DropTable(
                name: "DistrictMaster");

            migrationBuilder.DropTable(
                name: "LevelCount");

            migrationBuilder.DropTable(
                name: "LevelReport");

            migrationBuilder.DropTable(
                name: "LevelWiseMember");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "RegKeys");

            migrationBuilder.DropTable(
                name: "RewardMaster");

            migrationBuilder.DropTable(
                name: "RewardMasterDTO");

            migrationBuilder.DropTable(
                name: "StateMaster");
        }
    }
}
