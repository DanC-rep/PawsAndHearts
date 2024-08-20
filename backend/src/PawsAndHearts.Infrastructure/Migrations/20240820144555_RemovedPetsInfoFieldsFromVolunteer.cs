using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawsAndHearts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPetsInfoFieldsFromVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pets_being_treated",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "pets_found_home",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "pets_looking_for_home",
                table: "volunteers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pets_being_treated",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pets_found_home",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pets_looking_for_home",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
