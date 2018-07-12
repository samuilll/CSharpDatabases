using Microsoft.EntityFrameworkCore.Migrations;

namespace P03_FootballBetting.Data.Migrations
{
    public partial class Three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeTeamBetrate",
                table: "Games",
                newName: "HomeTeamBetRate");

            migrationBuilder.RenameColumn(
                name: "DrawBetrate",
                table: "Games",
                newName: "DrawBetRate");

            migrationBuilder.RenameColumn(
                name: "AwayTeamBetrate",
                table: "Games",
                newName: "AwayTeamBetRate");

            migrationBuilder.RenameColumn(
                name: "Prediciton",
                table: "Bets",
                newName: "Prediction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeTeamBetRate",
                table: "Games",
                newName: "HomeTeamBetrate");

            migrationBuilder.RenameColumn(
                name: "DrawBetRate",
                table: "Games",
                newName: "DrawBetrate");

            migrationBuilder.RenameColumn(
                name: "AwayTeamBetRate",
                table: "Games",
                newName: "AwayTeamBetrate");

            migrationBuilder.RenameColumn(
                name: "Prediction",
                table: "Bets",
                newName: "Prediciton");
        }
    }
}
