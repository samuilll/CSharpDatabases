using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamBuilder.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_UserId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_UserId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Invitations");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CreatorId",
                table: "Teams",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InvitedUserId",
                table: "Invitations",
                column: "InvitedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_InvitedUserId",
                table: "Invitations",
                column: "InvitedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_CreatorId",
                table: "Teams",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_InvitedUserId",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_CreatorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CreatorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_InvitedUserId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Invitations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserId",
                table: "Invitations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_UserId",
                table: "Invitations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
