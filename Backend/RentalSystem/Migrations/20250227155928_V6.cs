using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class V6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrSedista",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "Gorivo",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "Karoserija",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "Vrsta",
                table: "Vozila");

            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    BrSedista = table.Column<int>(type: "int", nullable: false),
                    Gorivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Karoserija = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Automobili_Vozila_ID",
                        column: x => x.ID,
                        principalTable: "Vozila",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Vrsta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motori", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Motori_Vozila_ID",
                        column: x => x.ID,
                        principalTable: "Vozila",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "Motori");

            migrationBuilder.AddColumn<int>(
                name: "BrSedista",
                table: "Vozila",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Vozila",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gorivo",
                table: "Vozila",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Karoserija",
                table: "Vozila",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vrsta",
                table: "Vozila",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
