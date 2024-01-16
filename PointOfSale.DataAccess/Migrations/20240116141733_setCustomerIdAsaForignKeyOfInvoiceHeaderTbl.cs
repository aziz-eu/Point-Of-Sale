using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Migrations
{
    public partial class setCustomerIdAsaForignKeyOfInvoiceHeaderTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegularCustomerId",
                table: "InvoiceHeaders");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "InvoiceHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHeaders_CustomerId",
                table: "InvoiceHeaders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHeaders_Customers_CustomerId",
                table: "InvoiceHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHeaders_Customers_CustomerId",
                table: "InvoiceHeaders");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceHeaders_CustomerId",
                table: "InvoiceHeaders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InvoiceHeaders");

            migrationBuilder.AddColumn<int>(
                name: "RegularCustomerId",
                table: "InvoiceHeaders",
                type: "int",
                nullable: true);
        }
    }
}
