using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarRezApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCalculatedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedResponse",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "WasValid",
                table: "GameHistory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpectedResponse",
                table: "GameHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "WasValid",
                table: "GameHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
