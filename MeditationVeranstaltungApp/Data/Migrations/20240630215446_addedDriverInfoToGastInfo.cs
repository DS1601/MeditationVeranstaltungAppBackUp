using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeditationVeranstaltungApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedDriverInfoToGastInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "Kontakts",
                columns: new[] { "Id", "Anrede", "Email", "Geschlecht", "Land", "Nachname", "Stadt", "Telefon", "Vorname" },
                values: new object[] { 2, 0, "jaspal.singht@meditationcamp.com", 0, "DE", "Singh", "Hamburg", "+49 754 5678 5789", "Jaspal" });
            
            migrationBuilder.UpdateData(
                     table: "GastInfos",
                     keyColumn: "Id",
                     keyValue: 1,
                     column: "FahrerKontaktId",
                     value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kontakts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "GastInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "FahrerKontaktId",
                value: null);
        }
    }
}
