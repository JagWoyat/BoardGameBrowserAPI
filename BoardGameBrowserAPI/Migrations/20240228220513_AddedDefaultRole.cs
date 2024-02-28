using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameBrowserAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3adca5a0-8151-421f-9188-7782fd0c6728", null, "Administrator", "ADMINISTRATOR" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3adca5a0-8151-421f-9188-7782fd0c6728");
        }
    }
}
