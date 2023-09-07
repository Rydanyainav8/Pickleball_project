using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pickleball_project.Migrations
{
    /// <inheritdoc />
    public partial class rectifbirthdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birhtdate",
                table: "Clients",
                newName: "Birthdate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Clients",
                newName: "Birhtdate");
        }
    }
}
