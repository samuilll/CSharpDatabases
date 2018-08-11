using Microsoft.EntityFrameworkCore.Migrations;

namespace PetClinic.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Passports_PassportSerialNumber",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerName",
                table: "Passports",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AnimalAids_Name",
                table: "AnimalAids",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Passports_PassportSerialNumber",
                table: "Animals",
                column: "PassportSerialNumber",
                principalTable: "Passports",
                principalColumn: "SerialNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Passports_PassportSerialNumber",
                table: "Animals");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AnimalAids_Name",
                table: "AnimalAids");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerName",
                table: "Passports",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Passports_PassportSerialNumber",
                table: "Animals",
                column: "PassportSerialNumber",
                principalTable: "Passports",
                principalColumn: "SerialNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
