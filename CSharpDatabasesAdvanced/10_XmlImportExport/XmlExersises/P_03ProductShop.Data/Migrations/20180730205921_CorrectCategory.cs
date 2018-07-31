using Microsoft.EntityFrameworkCore.Migrations;

namespace P_03ProductShop.Data.Migrations
{
    public partial class CorrectCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
