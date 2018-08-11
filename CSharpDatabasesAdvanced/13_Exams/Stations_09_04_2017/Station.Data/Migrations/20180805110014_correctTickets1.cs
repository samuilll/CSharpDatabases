using Microsoft.EntityFrameworkCore.Migrations;

namespace Travelling.Data.Migrations
{
    public partial class correctTickets1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CustomerCards_PersonalCardId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalCardId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CustomerCards_PersonalCardId",
                table: "Tickets",
                column: "PersonalCardId",
                principalTable: "CustomerCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CustomerCards_PersonalCardId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalCardId",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CustomerCards_PersonalCardId",
                table: "Tickets",
                column: "PersonalCardId",
                principalTable: "CustomerCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
