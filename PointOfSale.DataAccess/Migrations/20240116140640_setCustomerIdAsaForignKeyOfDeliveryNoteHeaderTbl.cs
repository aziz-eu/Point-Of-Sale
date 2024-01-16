using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Migrations
{
    public partial class setCustomerIdAsaForignKeyOfDeliveryNoteHeaderTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegularCustomerId",
                table: "DeliveryNoteHeaders");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "DeliveryNoteHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteHeaders_CustomerId",
                table: "DeliveryNoteHeaders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryNoteHeaders_Customers_CustomerId",
                table: "DeliveryNoteHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryNoteHeaders_Customers_CustomerId",
                table: "DeliveryNoteHeaders");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryNoteHeaders_CustomerId",
                table: "DeliveryNoteHeaders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "DeliveryNoteHeaders");

            migrationBuilder.AddColumn<int>(
                name: "RegularCustomerId",
                table: "DeliveryNoteHeaders",
                type: "int",
                nullable: true);
        }
    }
}
