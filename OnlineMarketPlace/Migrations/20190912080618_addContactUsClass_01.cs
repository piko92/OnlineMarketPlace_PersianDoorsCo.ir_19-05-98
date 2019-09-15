using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class addContactUsClass_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Brand",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    RegdDateTime = table.Column<DateTime>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    ApprovedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_AspNetUsers_ApprovedUserId",
                        column: x => x.ApprovedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_UserId",
                table: "Brand",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_ApprovedUserId",
                table: "ContactUs",
                column: "ApprovedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_AspNetUsers_UserId",
                table: "Brand",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_AspNetUsers_UserId",
                table: "Brand");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_Brand_UserId",
                table: "Brand");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Brand",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
