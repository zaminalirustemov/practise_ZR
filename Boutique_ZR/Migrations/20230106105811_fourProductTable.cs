using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boutique_ZR.Migrations
{
    public partial class fourProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Tags_TagId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Tags_TagId",
                table: "Products",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Tags_TagId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TagsId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Tags_TagId",
                table: "Products",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
