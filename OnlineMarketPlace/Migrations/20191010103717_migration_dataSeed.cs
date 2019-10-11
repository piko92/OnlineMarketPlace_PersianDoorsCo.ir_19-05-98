using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class migration_dataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "LatinName", "Name", "RatioPrice", "Status", "Symbol" },
                values: new object[,]
                {
                    { 1, "Rial", "ریال", null, true, "ريال" },
                    { 2, "Toman", "تومان", null, true, "تومان" },
                    { 3, "Dollar", "دلار", null, true, "$" },
                    { 4, "Euro", "یورو", null, true, "€" },
                    { 5, "Yuan", "یوان", null, true, "¥" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
