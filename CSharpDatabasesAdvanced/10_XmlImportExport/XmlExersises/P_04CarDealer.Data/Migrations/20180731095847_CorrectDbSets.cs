using Microsoft.EntityFrameworkCore.Migrations;

namespace P_04CarDealer.Data.Migrations
{
    public partial class CorrectDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Part_Supplier_SupplierId",
                table: "Part");

            migrationBuilder.DropForeignKey(
                name: "FK_PartCar_Car_CarId",
                table: "PartCar");

            migrationBuilder.DropForeignKey(
                name: "FK_PartCar_Part_PartId",
                table: "PartCar");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Car_CarId",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Customer_CustomerId",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartCar",
                table: "PartCar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Part",
                table: "Part");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "Sales");

            migrationBuilder.RenameTable(
                name: "PartCar",
                newName: "PartCars");

            migrationBuilder.RenameTable(
                name: "Part",
                newName: "Parts");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_CustomerId",
                table: "Sales",
                newName: "IX_Sales_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_CarId",
                table: "Sales",
                newName: "IX_Sales_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_PartCar_PartId",
                table: "PartCars",
                newName: "IX_PartCars_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_Part_SupplierId",
                table: "Parts",
                newName: "IX_Parts_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartCars",
                table: "PartCars",
                columns: new[] { "CarId", "PartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartCars_Cars_CarId",
                table: "PartCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCars_Parts_PartId",
                table: "PartCars",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Suppliers_SupplierId",
                table: "Parts",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Cars_CarId",
                table: "Sales",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCars_Cars_CarId",
                table: "PartCars");

            migrationBuilder.DropForeignKey(
                name: "FK_PartCars_Parts_PartId",
                table: "PartCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Suppliers_SupplierId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Cars_CarId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartCars",
                table: "PartCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Sale");

            migrationBuilder.RenameTable(
                name: "Parts",
                newName: "Part");

            migrationBuilder.RenameTable(
                name: "PartCars",
                newName: "PartCar");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CustomerId",
                table: "Sale",
                newName: "IX_Sale_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CarId",
                table: "Sale",
                newName: "IX_Sale_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_SupplierId",
                table: "Part",
                newName: "IX_Part_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_PartCars_PartId",
                table: "PartCar",
                newName: "IX_PartCar_PartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Part",
                table: "Part",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartCar",
                table: "PartCar",
                columns: new[] { "CarId", "PartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Part_Supplier_SupplierId",
                table: "Part",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCar_Car_CarId",
                table: "PartCar",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCar_Part_PartId",
                table: "PartCar",
                column: "PartId",
                principalTable: "Part",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Car_CarId",
                table: "Sale",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Customer_CustomerId",
                table: "Sale",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
