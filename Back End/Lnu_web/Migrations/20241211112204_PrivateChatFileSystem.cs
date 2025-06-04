using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lnu_web.Migrations
{
    /// <inheritdoc />
    public partial class PrivateChatFileSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "CoreStudentDbo",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<string>(type: "longtext", nullable: false),
                    FileName = table.Column<string>(type: "longtext", nullable: false),
                    FilePath = table.Column<string>(type: "longtext", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrivateChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    MessageId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SenderId = table.Column<string>(type: "longtext", nullable: false),
                    ReceiverId = table.Column<string>(type: "longtext", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    isRead = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChats", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "PrivateChats");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "CoreStudentDbo");
        }
    }
}
