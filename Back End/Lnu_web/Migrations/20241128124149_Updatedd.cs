using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lnu_web.Migrations
{
    /// <inheritdoc />
    public partial class Updatedd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CoreTeacherDbo",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "CoreTeacherDbo");
        }
    }
}
