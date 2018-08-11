using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travelling.Data.Migrations
{
    public partial class AddTown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TownId",
                table: "Stations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Town",
                columns: table => new
                {
                    TownId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.TownId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stations_TownId",
                table: "Stations",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Town_TownId",
                table: "Stations",
                column: "TownId",
                principalTable: "Town",
                principalColumn: "TownId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Town_TownId",
                table: "Stations");

            migrationBuilder.DropTable(
                name: "Town");

            migrationBuilder.DropIndex(
                name: "IX_Stations_TownId",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "Stations");
        }
    }
}
