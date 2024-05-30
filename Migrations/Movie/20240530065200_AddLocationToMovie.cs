using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketingApplication.Migrations.Movie
{
    /// <inheritdoc />
    public partial class AddLocationToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Movies");
        }
    }
}
