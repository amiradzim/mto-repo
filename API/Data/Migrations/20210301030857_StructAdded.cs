using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class StructAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjName = table.Column<string>(type: "TEXT", nullable: true),
                    PlatName = table.Column<string>(type: "TEXT", nullable: true),
                    StructType = table.Column<string>(type: "TEXT", nullable: true),
                    StructArea = table.Column<string>(type: "TEXT", nullable: true),
                    PlatArea = table.Column<string>(type: "TEXT", nullable: true),
                    SubArea = table.Column<string>(type: "TEXT", nullable: true),
                    MatType = table.Column<string>(type: "TEXT", nullable: true),
                    MatVariant = table.Column<string>(type: "TEXT", nullable: true),
                    ProcMethod = table.Column<string>(type: "TEXT", nullable: true),
                    DwgNo = table.Column<string>(type: "TEXT", nullable: true),
                    DwgCode = table.Column<string>(type: "TEXT", nullable: true),
                    MatGroup = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Diameter = table.Column<float>(type: "REAL", nullable: false),
                    Thickness = table.Column<float>(type: "REAL", nullable: false),
                    Nal = table.Column<float>(type: "REAL", nullable: false),
                    UnitWeight = table.Column<float>(type: "REAL", nullable: false),
                    BaseWeight = table.Column<float>(type: "REAL", nullable: false),
                    SurfaceArea = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    KnownAs = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastActive = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
