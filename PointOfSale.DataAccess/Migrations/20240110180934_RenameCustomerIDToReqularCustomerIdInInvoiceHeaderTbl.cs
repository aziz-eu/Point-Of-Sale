using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Migrations
{
    public partial class RenameCustomerIDToReqularCustomerIdInInvoiceHeaderTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "InvoiceHeaders",
                newName: "RegularCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegularCustomerId",
                table: "InvoiceHeaders",
                newName: "CustomerId");
        }
    }
}
