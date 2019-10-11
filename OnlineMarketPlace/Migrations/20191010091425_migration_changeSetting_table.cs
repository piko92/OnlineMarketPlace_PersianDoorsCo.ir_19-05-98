using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class migration_changeSetting_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SMSApiNumber",
                table: "Setting",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SMSApiNumber",
                table: "Setting");
        }
    }
}
