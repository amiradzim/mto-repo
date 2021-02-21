using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ChangedColumnNames : Migration
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
                    PlatNo = table.Column<string>(type: "TEXT", nullable: true),
                    PlatType = table.Column<string>(type: "TEXT", nullable: true),
                    PlatArea = table.Column<string>(type: "TEXT", nullable: true),
                    SubArea = table.Column<string>(type: "TEXT", nullable: true),
                    MatType = table.Column<string>(type: "TEXT", nullable: true),
                    MatVariant = table.Column<string>(type: "TEXT", nullable: true),
                    ProcMethod = table.Column<string>(type: "TEXT", nullable: true),
                    DwgNo = table.Column<string>(type: "TEXT", nullable: true),
                    DwgCode = table.Column<string>(type: "TEXT", nullable: true),
                    MatGroup = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Diameter = table.Column<long>(type: "INTEGER", nullable: false),
                    Thickness = table.Column<long>(type: "INTEGER", nullable: false),
                    Nal = table.Column<long>(type: "INTEGER", nullable: false),
                    UnitWeight = table.Column<long>(type: "INTEGER", nullable: false),
                    BaseWeight = table.Column<long>(type: "INTEGER", nullable: false),
                    SurfaceArea = table.Column<long>(type: "INTEGER", nullable: false)
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
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true)
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
