using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class contactuUsRev01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_UserId",
                table: "ContactUs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_ContactUs_UserId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContactUs");
        }
    }
}
