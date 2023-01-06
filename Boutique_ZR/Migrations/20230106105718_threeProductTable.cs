using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boutique_ZR.Migrations
{
    public partial class threeProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoriesId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Tags_TagsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TagsId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "categoriesId",
                table: "Products",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_categoriesId",
                table: "Products",
                newName: "IX_Products_TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Tags_TagId",
                table: "Products",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Tags_TagId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "Products",
                newName: "categoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TagId",
                table: "Products",
                newName: "IX_Products_categoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TagsId",
                table: "Products",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoriesId",
                table: "Products",
                column: "categoriesId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Tags_TagsId",
                table: "Products",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
