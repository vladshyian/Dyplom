using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lnu_web.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentId",
                table: "CoreTeacherDbo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DepartamentAboutDbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DepartamentAbout = table.Column<string>(type: "longtext", nullable: false),
                    DepartamentMission = table.Column<string>(type: "longtext", nullable: false),
                    DepartamentVizia = table.Column<string>(type: "longtext", nullable: false),
                    DepartamnetStrategy = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentAboutDbo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DepartamentsListDbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DepartamentName = table.Column<string>(type: "longtext", nullable: false),
                    TeacherName = table.Column<string>(type: "longtext", nullable: false),
                    KafedtaPhoto = table.Column<string>(type: "longtext", nullable: false),
                    DepartamentPhone = table.Column<string>(type: "longtext", nullable: false),
                    DepartamentLink = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentsListDbo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "departaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DepartamentAboutId = table.Column<int>(type: "int", nullable: false),
                    DepartamentsListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_departaments_DepartamentAboutDbo_DepartamentAboutId",
                        column: x => x.DepartamentAboutId,
                        principalTable: "DepartamentAboutDbo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_departaments_DepartamentsListDbo_DepartamentsListId",
                        column: x => x.DepartamentsListId,
                        principalTable: "DepartamentsListDbo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_departaments_DepartamentAboutId",
                table: "departaments",
                column: "DepartamentAboutId");

            migrationBuilder.CreateIndex(
                name: "IX_departaments_DepartamentsListId",
                table: "departaments",
                column: "DepartamentsListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departaments");

            migrationBuilder.DropTable(
                name: "DepartamentAboutDbo");

            migrationBuilder.DropTable(
                name: "DepartamentsListDbo");

            migrationBuilder.DropColumn(
                name: "DepartamentId",
                table: "CoreTeacherDbo");
        }
    }
}
