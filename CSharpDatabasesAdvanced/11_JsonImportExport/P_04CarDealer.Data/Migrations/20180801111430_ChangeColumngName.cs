using Microsoft.EntityFrameworkCore.Migrations;

namespace P_04CarDealer.Data.Migrations
{
    public partial class ChangeColumngName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "Customers",
                newName: "BirthDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Customers",
                newName: "BirthDay");
        }
    }
}
