using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawsAndHearts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedColumnNamesOnPetAndVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone_number_value",
                table: "volunteers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "full_name_surname",
                table: "volunteers",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "full_name_patronymic",
                table: "volunteers",
                newName: "patronymic");

            migrationBuilder.RenameColumn(
                name: "full_name_name",
                table: "volunteers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "phone_number_value",
                table: "pets",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "creation_date_value",
                table: "pets",
                newName: "creation_date");

            migrationBuilder.RenameColumn(
                name: "birth_date_value",
                table: "pets",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "address_street",
                table: "pets",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "address_house",
                table: "pets",
                newName: "house");

            migrationBuilder.RenameColumn(
                name: "address_flat",
                table: "pets",
                newName: "flat");

            migrationBuilder.RenameColumn(
                name: "address_city",
                table: "pets",
                newName: "city");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "volunteers",
                newName: "full_name_surname");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "volunteers",
                newName: "phone_number_value");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "volunteers",
                newName: "full_name_patronymic");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "volunteers",
                newName: "full_name_name");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "pets",
                newName: "address_street");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "pets",
                newName: "phone_number_value");

            migrationBuilder.RenameColumn(
                name: "house",
                table: "pets",
                newName: "address_house");

            migrationBuilder.RenameColumn(
                name: "flat",
                table: "pets",
                newName: "address_flat");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "pets",
                newName: "creation_date_value");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "pets",
                newName: "address_city");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "pets",
                newName: "birth_date_value");
        }
    }
}
