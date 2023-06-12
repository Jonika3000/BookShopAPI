using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class Big : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_publishingHouses_PublishingHouseId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_publishingHouses",
                table: "publishingHouses");

            migrationBuilder.RenameTable(
                name: "publishingHouses",
                newName: "PublishingHouses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublishingHouses",
                table: "PublishingHouses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_PublishingHouses_PublishingHouseId",
                table: "Books",
                column: "PublishingHouseId",
                principalTable: "PublishingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_PublishingHouses_PublishingHouseId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublishingHouses",
                table: "PublishingHouses");

            migrationBuilder.RenameTable(
                name: "PublishingHouses",
                newName: "publishingHouses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_publishingHouses",
                table: "publishingHouses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_publishingHouses_PublishingHouseId",
                table: "Books",
                column: "PublishingHouseId",
                principalTable: "publishingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
