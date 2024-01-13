using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Migrations
{
    public partial class setConstantforProductDelectRestectionWhenProductIsAvailableInDeliveryNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNoteDetails_Products_ProductId",
                table: "DeliveryNoteDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNoteDetails_Products_ProductId",
                table: "DeliveryNoteDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNoteDetails_Products_ProductId",
                table: "DeliveryNoteDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNoteDetails_Products_ProductId",
                table: "DeliveryNoteDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
