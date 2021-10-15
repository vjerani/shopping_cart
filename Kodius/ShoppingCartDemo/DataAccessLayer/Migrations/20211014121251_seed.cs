using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartDemo.DataAccessLayer.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 1, "Smart Hub", 49.99f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 2, "Motion Sensor", 24.99f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 3, "Wireless Camera", 99.99f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 4, "Smoke Sensor", 19.99f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 5, "Water Leak Sensor", 14.99f });

            migrationBuilder.InsertData(
                table: "PromotionCodes",
                columns: new[] { "Id", "Amount", "IsDiscount", "Name", "UseInConjuction" },
                values: new object[] { 1, 0.2f, true, "20%OFF", false });

            migrationBuilder.InsertData(
                table: "PromotionCodes",
                columns: new[] { "Id", "Amount", "IsDiscount", "Name", "UseInConjuction" },
                values: new object[] { 2, 0.05f, true, "5%OFF", true });

            migrationBuilder.InsertData(
                table: "PromotionCodes",
                columns: new[] { "Id", "Amount", "IsDiscount", "Name", "UseInConjuction" },
                values: new object[] { 3, 20f, false, "20EUROFF", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PromotionCodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PromotionCodes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PromotionCodes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
