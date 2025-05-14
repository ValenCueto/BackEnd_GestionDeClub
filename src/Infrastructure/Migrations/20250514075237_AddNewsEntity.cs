using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtAvailabilities_Courts_CourtId",
                table: "CourtAvailabilities");

            migrationBuilder.DropIndex(
                name: "IX_CourtAvailabilities_CourtId",
                table: "CourtAvailabilities");

            migrationBuilder.DropColumn(
                name: "CourtId",
                table: "CourtAvailabilities");

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.AddColumn<int>(
                name: "CourtId",
                table: "CourtAvailabilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourtAvailabilities_CourtId",
                table: "CourtAvailabilities",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtAvailabilities_Courts_CourtId",
                table: "CourtAvailabilities",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id");
        }
    }
}
