using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeditationVeranstaltungApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28aaaaad-012d-4951-b501-5813b2a541ff", null, "Admin", "ADMIN" },
                    { "a609455d-5f11-42bd-be9d-5fd66e4570c0", null, "User", "USER" },
                    { "b2b71169-bcf7-41c6-90c5-3f808fd7cafa", null, "Driver", "DRIVER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ec673be3-bdfe-4ee6-9ff1-3712a38acb5e", 0, "c1884fc7-ab90-40b4-9839-5fb4a80f8f03", "admin@web.de", false, "Admin", "", false, null, "ADMIN@WEB.DE", "ADMIN@WEB.DE", "AQAAAAIAAYagAAAAEMQRDvPi3JdHIUC35tKHMuUAUiE26dBU0efpeS1h8rNI5MlcNN5aDGAa+vuWMHRpEw==", null, false, "7f6ec9e0-ae29-4c2c-b1ed-f03f49e04e63", false, "admin@web.de" });

            migrationBuilder.UpdateData(
                table: "GastInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AnzahlMaenner", "AnzahlWeiblich" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "28aaaaad-012d-4951-b501-5813b2a541ff", "ec673be3-bdfe-4ee6-9ff1-3712a38acb5e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a609455d-5f11-42bd-be9d-5fd66e4570c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2b71169-bcf7-41c6-90c5-3f808fd7cafa");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "28aaaaad-012d-4951-b501-5813b2a541ff", "ec673be3-bdfe-4ee6-9ff1-3712a38acb5e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28aaaaad-012d-4951-b501-5813b2a541ff");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ec673be3-bdfe-4ee6-9ff1-3712a38acb5e");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "GastInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AnzahlMaenner", "AnzahlWeiblich" },
                values: new object[] { 0, 0 });
        }
    }
}
