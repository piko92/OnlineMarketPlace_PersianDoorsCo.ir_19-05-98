using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class generalpage02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DocumentFile",
                table: "GeneralPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "GeneralPage",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MovieFile",
                table: "GeneralPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoviePath",
                table: "GeneralPage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentFile",
                table: "GeneralPage");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "GeneralPage");

            migrationBuilder.DropColumn(
                name: "MovieFile",
                table: "GeneralPage");

            migrationBuilder.DropColumn(
                name: "MoviePath",
                table: "GeneralPage");
        }
    }
}
