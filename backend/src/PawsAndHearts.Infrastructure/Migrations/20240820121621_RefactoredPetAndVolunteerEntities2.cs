using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawsAndHearts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredPetAndVolunteerEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Requisites",
                table: "pets",
                newName: "requisites");

            migrationBuilder.RenameColumn(
                name: "PetPhotos",
                table: "pets",
                newName: "pet_photos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "pets",
                newName: "Requisites");

            migrationBuilder.RenameColumn(
                name: "pet_photos",
                table: "pets",
                newName: "PetPhotos");
        }
    }
}
