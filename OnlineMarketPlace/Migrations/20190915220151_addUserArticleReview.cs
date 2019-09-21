using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class addUserArticleReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_AspNetUsers_ApprovedUserId",
                table: "ContactUs");

            migrationBuilder.RenameColumn(
                name: "ApprovedUserId",
                table: "ContactUs",
                newName: "ApprovedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactUs_ApprovedUserId",
                table: "ContactUs",
                newName: "IX_ContactUs_ApprovedByUserId");

            migrationBuilder.CreateTable(
                name: "UserArticleReview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    AnonymousUserEmail = table.Column<string>(nullable: true),
                    AnonymousUserIp = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Fake = table.Column<bool>(nullable: true),
                    FakeUserName = table.Column<string>(nullable: true),
                    FakeDateTime = table.Column<DateTime>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    ApprovedByUserId = table.Column<string>(nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(nullable: true),
                    OpenComment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticleReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArticleReview_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserArticleReview_UserArticleReview_ParentId",
                        column: x => x.ParentId,
                        principalTable: "UserArticleReview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserArticleReview_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_ApprovedByUserId",
                table: "UserArticleReview",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_ParentId",
                table: "UserArticleReview",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_UserId",
                table: "UserArticleReview",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_ApprovedByUserId",
                table: "ContactUs",
                column: "ApprovedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_AspNetUsers_ApprovedByUserId",
                table: "ContactUs");

            migrationBuilder.DropTable(
                name: "UserArticleReview");

            migrationBuilder.RenameColumn(
                name: "ApprovedByUserId",
                table: "ContactUs",
                newName: "ApprovedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactUs_ApprovedByUserId",
                table: "ContactUs",
                newName: "IX_ContactUs_ApprovedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_ApprovedUserId",
                table: "ContactUs",
                column: "ApprovedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
