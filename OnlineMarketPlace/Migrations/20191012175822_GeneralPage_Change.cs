using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class GeneralPage_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "MainImage",
                table: "GeneralPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImagePath",
                table: "GeneralPage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "GeneralPage");

            migrationBuilder.DropColumn(
                name: "MainImagePath",
                table: "GeneralPage");
        }
    }
}
