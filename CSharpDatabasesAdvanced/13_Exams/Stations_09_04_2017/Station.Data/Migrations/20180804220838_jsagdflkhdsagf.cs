using Microsoft.EntityFrameworkCore.Migrations;

namespace Travelling.Data.Migrations
{
    public partial class jsagdflkhdsagf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainSeatClasses_SeatingClasses_TrainId",
                table: "TrainSeatClasses");

            migrationBuilder.CreateIndex(
                name: "IX_TrainSeatClasses_SeatingClassId",
                table: "TrainSeatClasses",
                column: "SeatingClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainSeatClasses_SeatingClasses_SeatingClassId",
                table: "TrainSeatClasses",
                column: "SeatingClassId",
                principalTable: "SeatingClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainSeatClasses_SeatingClasses_SeatingClassId",
                table: "TrainSeatClasses");

            migrationBuilder.DropIndex(
                name: "IX_TrainSeatClasses_SeatingClassId",
                table: "TrainSeatClasses");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainSeatClasses_SeatingClasses_TrainId",
                table: "TrainSeatClasses",
                column: "TrainId",
                principalTable: "SeatingClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
