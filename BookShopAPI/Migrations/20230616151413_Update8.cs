using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Logs",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "action",
                table: "Logs",
                newName: "Action");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Logs",
                newName: "dateTime");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "Logs",
                newName: "action");
        }
    }
}
