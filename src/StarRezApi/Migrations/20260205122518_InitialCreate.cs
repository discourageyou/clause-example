using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarRezApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KidNumber = table.Column<int>(type: "int", nullable: false),
                    KidResponse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpectedResponse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WasValid = table.Column<bool>(type: "bit", nullable: false),
                    ValidatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameHistory_KidNumber",
                table: "GameHistory",
                column: "KidNumber");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistory_ValidatedAt",
                table: "GameHistory",
                column: "ValidatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameHistory");
        }
    }
}
