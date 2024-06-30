using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeditationVeranstaltungApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedGastInfoWithContactTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontakts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anrede = table.Column<int>(type: "int", nullable: false),
                    Vorname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Geschlecht = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stadt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Land = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GastInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Veranstalltung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnzahlMaenner = table.Column<int>(type: "int", nullable: false),
                    AnzahlWeiblich = table.Column<int>(type: "int", nullable: false),
                    AnkunftAm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnkunftOrt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AbfahrtAm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AbfahrtOrt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AbgesagtAm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AbsageGrund = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notiz = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    KontaktId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FahrerKontaktId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GastInfos_Kontakts_FahrerKontaktId",
                        column: x => x.FahrerKontaktId,
                        principalTable: "Kontakts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GastInfos_Kontakts_KontaktId",
                        column: x => x.KontaktId,
                        principalTable: "Kontakts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kontakts",
                columns: new[] { "Id", "Anrede", "Email", "Geschlecht", "Land", "Nachname", "Stadt", "Telefon", "Vorname" },
                values: new object[] { 1, 0, "parminder.singht@meditationcamp.com", 0, "UK", "Singh", "Slough", "+44 123 5678 9911", "Parminder" });

            migrationBuilder.InsertData(
                table: "GastInfos",
                columns: new[] { "Id", "AbfahrtAm", "AbfahrtOrt", "AbgesagtAm", "AbsageGrund", "AnkunftAm", "AnkunftOrt", "AnzahlMaenner", "AnzahlWeiblich", "FahrerKontaktId", "KontaktId", "Notiz", "UserId", "Veranstalltung" },
                values: new object[] { 1, new DateTime(2024, 8, 14, 18, 20, 55, 630, DateTimeKind.Local), "Flughagen", null, null, new DateTime(2024, 8, 14, 18, 20, 55, 630, DateTimeKind.Local), "Flughagen", 0, 0, null, 1, null, "93df0a45-7232-45e6-b882-8dcc0966ba8a", "SAS2024HH" });

            migrationBuilder.CreateIndex(
                name: "IX_GastInfos_FahrerKontaktId",
                table: "GastInfos",
                column: "FahrerKontaktId");

            migrationBuilder.CreateIndex(
                name: "IX_GastInfos_KontaktId",
                table: "GastInfos",
                column: "KontaktId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GastInfos");

            migrationBuilder.DropTable(
                name: "Kontakts");
        }
    }
}
