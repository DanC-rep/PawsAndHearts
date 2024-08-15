using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawsAndHearts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredPetAndVolunteerEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pet_photos");

            migrationBuilder.DropTable(
                name: "requisites");

            migrationBuilder.DropTable(
                name: "social_networks");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "volunteers",
                newName: "full_name_surname");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "volunteers",
                newName: "full_name_patronymic");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "volunteers",
                newName: "full_name_name");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "volunteers",
                newName: "phone_number_value");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "pets",
                newName: "phone_number_value");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "pets",
                newName: "creation_date_value");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "pets",
                newName: "birth_date_value");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "pets",
                newName: "address_street");

            migrationBuilder.AddColumn<string>(
                name: "VolunteerDetails",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetDetails",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_city",
                table: "pets",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_flat",
                table: "pets",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_house",
                table: "pets",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteerDetails",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "PetDetails",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "address_city",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "address_flat",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "address_house",
                table: "pets");

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
                table: "volunteers",
                newName: "phone");

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
                newName: "address");

            migrationBuilder.CreateTable(
                name: "pet_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    path = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pet_photos_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requisites",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requisites", x => x.id);
                    table.ForeignKey(
                        name: "fk_requisites_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_requisites_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "social_networks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    link = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_networks", x => x.id);
                    table.ForeignKey(
                        name: "fk_social_networks_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pet_photos_pet_id",
                table: "pet_photos",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_requisites_pet_id",
                table: "requisites",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_requisites_volunteer_id",
                table: "requisites",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_social_networks_volunteer_id",
                table: "social_networks",
                column: "volunteer_id");
        }
    }
}
