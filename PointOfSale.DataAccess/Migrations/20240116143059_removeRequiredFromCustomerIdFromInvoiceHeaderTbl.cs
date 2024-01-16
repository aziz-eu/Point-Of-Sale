using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Migrations
{
    public partial class removeRequiredFromCustomerIdFromInvoiceHeaderTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNoteHeaders_Customers_CustomerId",
                table: "DeliveryNoteHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHeaders_Customers_CustomerId",
                table: "InvoiceHeaders");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "InvoiceHeaders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "DeliveryNoteHeaders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNoteHeaders_Customers_CustomerId",
                table: "DeliveryNoteHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHeaders_Customers_CustomerId",
                table: "InvoiceHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNoteHeaders_Customers_CustomerId",
                table: "DeliveryNoteHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHeaders_Customers_CustomerId",
                table: "InvoiceHeaders");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "InvoiceHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "DeliveryNoteHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNoteHeaders_Customers_CustomerId",
                table: "DeliveryNoteHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHeaders_Customers_CustomerId",
                table: "InvoiceHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
