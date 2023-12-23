using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.DataAccess.Migrations
{
    public partial class vatNCompanyInfoCrateWhenModelCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Address", "AribicAddress", "AribicName", "AribicPhoneNumber1", "AribicPhoneNumber2", "AribicPostOfficeNo", "ClstTRN", "Email", "Name", "PhoneNumber1", "PhoneNumber2", "PostOfficeNo", "TRN" },
                values: new object[] { 1, "something, UAE", "someting", "Company Name", "1234567890", "1234567890", "1234567890", "123456789", "someting@mail.com", "Company Name", "1234567890", "1234567890", "1234567890", "123456789" });

            migrationBuilder.InsertData(
                table: "VatRates",
                columns: new[] { "Id", "Vat" },
                values: new object[] { 1, 5.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VatRates",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
