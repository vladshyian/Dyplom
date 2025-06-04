using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lnu_web.Migrations
{
    /// <inheritdoc />
    public partial class SenderNameInMessageDbo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Messages",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Messages");
        }
    }
}
