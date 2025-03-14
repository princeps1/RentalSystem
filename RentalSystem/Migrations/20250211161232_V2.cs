using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikVozilo");

            migrationBuilder.AddColumn<int>(
                name: "KorisnikID",
                table: "Vozila",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_KorisnikID",
                table: "Vozila",
                column: "KorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_Korisnici_KorisnikID",
                table: "Vozila",
                column: "KorisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_Korisnici_KorisnikID",
                table: "Vozila");

            migrationBuilder.DropIndex(
                name: "IX_Vozila_KorisnikID",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "KorisnikID",
                table: "Vozila");

            migrationBuilder.CreateTable(
                name: "KorisnikVozilo",
                columns: table => new
                {
                    KorisniciID = table.Column<int>(type: "int", nullable: false),
                    VozilaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikVozilo", x => new { x.KorisniciID, x.VozilaID });
                    table.ForeignKey(
                        name: "FK_KorisnikVozilo_Korisnici_KorisniciID",
                        column: x => x.KorisniciID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisnikVozilo_Vozila_VozilaID",
                        column: x => x.VozilaID,
                        principalTable: "Vozila",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikVozilo_VozilaID",
                table: "KorisnikVozilo",
                column: "VozilaID");
        }
    }
}
