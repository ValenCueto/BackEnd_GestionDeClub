using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourtEntityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courts",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Courts",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtAvailabilities_Courts_CourtId",
                table: "CourtAvailabilities");

            migrationBuilder.DropIndex(
                name: "IX_CourtAvailabilities_CourtId",
                table: "CourtAvailabilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "CourtId",
                table: "CourtAvailabilities");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Courts",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
