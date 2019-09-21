using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class PFeatures01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdditionalFeatures_ProductAbstract_ProductAbstractId",
                table: "ProductAdditionalFeatures");

            migrationBuilder.RenameColumn(
                name: "ProductAbstractId",
                table: "ProductAdditionalFeatures",
                newName: "ProductFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAdditionalFeatures_ProductAbstractId",
                table: "ProductAdditionalFeatures",
                newName: "IX_ProductAdditionalFeatures_ProductFeatureId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ScreenResulation",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "ScreenResulation",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalFeaturesId",
                table: "ProductFeature",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalFeaturesId",
                table: "ProductAdditionalFeatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdditionalFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    ImageIcon = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalFeatures_AdditionalFeatures_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AdditionalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalFeatures_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_AdditionalFeaturesId",
                table: "ProductFeature",
                column: "AdditionalFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalFeatures_AdditionalFeaturesId",
                table: "ProductAdditionalFeatures",
                column: "AdditionalFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFeatures_ParentId",
                table: "AdditionalFeatures",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFeatures_UserId",
                table: "AdditionalFeatures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdditionalFeatures_AdditionalFeatures_AdditionalFeaturesId",
                table: "ProductAdditionalFeatures",
                column: "AdditionalFeaturesId",
                principalTable: "AdditionalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdditionalFeatures_ProductFeature_ProductFeatureId",
                table: "ProductAdditionalFeatures",
                column: "ProductFeatureId",
                principalTable: "ProductFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeature_AdditionalFeatures_AdditionalFeaturesId",
                table: "ProductFeature",
                column: "AdditionalFeaturesId",
                principalTable: "AdditionalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdditionalFeatures_AdditionalFeatures_AdditionalFeaturesId",
                table: "ProductAdditionalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdditionalFeatures_ProductFeature_ProductFeatureId",
                table: "ProductAdditionalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeature_AdditionalFeatures_AdditionalFeaturesId",
                table: "ProductFeature");

            migrationBuilder.DropTable(
                name: "AdditionalFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeature_AdditionalFeaturesId",
                table: "ProductFeature");

            migrationBuilder.DropIndex(
                name: "IX_ProductAdditionalFeatures_AdditionalFeaturesId",
                table: "ProductAdditionalFeatures");

            migrationBuilder.DropColumn(
                name: "AdditionalFeaturesId",
                table: "ProductFeature");

            migrationBuilder.DropColumn(
                name: "AdditionalFeaturesId",
                table: "ProductAdditionalFeatures");

            migrationBuilder.RenameColumn(
                name: "ProductFeatureId",
                table: "ProductAdditionalFeatures",
                newName: "ProductAbstractId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAdditionalFeatures_ProductFeatureId",
                table: "ProductAdditionalFeatures",
                newName: "IX_ProductAdditionalFeatures_ProductAbstractId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ScreenResulation",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "ScreenResulation",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdditionalFeatures_ProductAbstract_ProductAbstractId",
                table: "ProductAdditionalFeatures",
                column: "ProductAbstractId",
                principalTable: "ProductAbstract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
