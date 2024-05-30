using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketingApplication.Migrations.Movie
{
    /// <inheritdoc />
    public partial class UpdateLocationToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationSerialized",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationSerialized",
                table: "Movies");
        }
    }
}
