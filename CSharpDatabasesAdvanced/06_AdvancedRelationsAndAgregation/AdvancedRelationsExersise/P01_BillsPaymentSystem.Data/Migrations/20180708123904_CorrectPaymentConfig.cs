using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_BillsPaymentSystem.Data.Migrations
{
    public partial class CorrectPaymentConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_CreditCards_CreditCardId",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_CreditCardId",
                table: "PaymentMethods");

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "PaymentMethods",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_CreditCardId",
                table: "PaymentMethods",
                column: "CreditCardId",
                unique: true,
                filter: "[CreditCardId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_CreditCards_CreditCardId",
                table: "PaymentMethods",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_CreditCards_CreditCardId",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_CreditCardId",
                table: "PaymentMethods");

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "PaymentMethods",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_CreditCardId",
                table: "PaymentMethods",
                column: "CreditCardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_CreditCards_CreditCardId",
                table: "PaymentMethods",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
