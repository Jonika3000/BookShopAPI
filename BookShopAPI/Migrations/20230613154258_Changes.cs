using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorEntityId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorEntityId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorEntityId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Sales",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Sales",
                newName: "Adress");

            migrationBuilder.AddColumn<int>(
                name: "AuthorEntityId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorEntityId",
                table: "Books",
                column: "AuthorEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorEntityId",
                table: "Books",
                column: "AuthorEntityId",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}
