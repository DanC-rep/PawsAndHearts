using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawsAndHearts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pet_photos_pets_pet_id",
                table: "pet_photos");

            migrationBuilder.DropForeignKey(
                name: "fk_requisites_pets_pet_id",
                table: "requisites");

            migrationBuilder.DropForeignKey(
                name: "fk_requisites_volunteers_volunteer_id",
                table: "requisites");

            migrationBuilder.DropForeignKey(
                name: "fk_social_networks_volunteers_volunteer_id",
                table: "social_networks");

            migrationBuilder.AlterColumn<Guid>(
                name: "volunteer_id",
                table: "social_networks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "volunteer_id",
                table: "requisites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "pet_id",
                table: "requisites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "help_status",
                table: "pets",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "pet_id",
                table: "pet_photos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_pet_photos_pets_pet_id",
                table: "pet_photos",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_requisites_pets_pet_id",
                table: "requisites",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_requisites_volunteers_volunteer_id",
                table: "requisites",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_social_networks_volunteers_volunteer_id",
                table: "social_networks",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pet_photos_pets_pet_id",
                table: "pet_photos");

            migrationBuilder.DropForeignKey(
                name: "fk_requisites_pets_pet_id",
                table: "requisites");

            migrationBuilder.DropForeignKey(
                name: "fk_requisites_volunteers_volunteer_id",
                table: "requisites");

            migrationBuilder.DropForeignKey(
                name: "fk_social_networks_volunteers_volunteer_id",
                table: "social_networks");

            migrationBuilder.AlterColumn<Guid>(
                name: "volunteer_id",
                table: "social_networks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "volunteer_id",
                table: "requisites",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "pet_id",
                table: "requisites",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "help_status",
                table: "pets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "pet_id",
                table: "pet_photos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_pet_photos_pets_pet_id",
                table: "pet_photos",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_requisites_pets_pet_id",
                table: "requisites",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_requisites_volunteers_volunteer_id",
                table: "requisites",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_social_networks_volunteers_volunteer_id",
                table: "social_networks",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id");
        }
    }
}
