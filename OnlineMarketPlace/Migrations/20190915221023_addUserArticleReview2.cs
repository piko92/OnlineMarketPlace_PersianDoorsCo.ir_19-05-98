using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class addUserArticleReview2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "UserArticleReview",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_ArticleId",
                table: "UserArticleReview",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserArticleReview_Article_ArticleId",
                table: "UserArticleReview",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserArticleReview_Article_ArticleId",
                table: "UserArticleReview");

            migrationBuilder.DropIndex(
                name: "IX_UserArticleReview_ArticleId",
                table: "UserArticleReview");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "UserArticleReview");
        }
    }
}
