using Microsoft.EntityFrameworkCore.Migrations;

namespace Travelling.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Abbreviation",
                table: "SeatingClasses",
                type: "Char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Char(2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Abbreviation",
                table: "SeatingClasses",
                type: "Char(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Char(2)");
        }
    }
}
