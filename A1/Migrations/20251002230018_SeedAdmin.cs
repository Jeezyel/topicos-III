using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace A1.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b14a6f1-ef0b-4c89-b6b0-def987654321", null, "Usuario", "USUARIO" },
                    { "f4dbf4dd-1df8-4e6a-9a15-abc123456789", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1e8e011d-7fde-4b16-8078-6775d7fca57e", 0, "c3aef999-96de-4a7f-9c67-abcdef123456", "admin@admin.com", true, false, null, "Administrador", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAENxiM+eS0Ag9KL6O40a1TEUpV+jH0nxCFioLIPdrOJ9Y5x2Sx28OaWLn8dHwCML5nQ==", null, false, "d6f5c999-46de-4a7f-9c67-123456789abc", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f4dbf4dd-1df8-4e6a-9a15-abc123456789", "1e8e011d-7fde-4b16-8078-6775d7fca57e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b14a6f1-ef0b-4c89-b6b0-def987654321");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f4dbf4dd-1df8-4e6a-9a15-abc123456789", "1e8e011d-7fde-4b16-8078-6775d7fca57e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4dbf4dd-1df8-4e6a-9a15-abc123456789");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e8e011d-7fde-4b16-8078-6775d7fca57e");
        }
    }
}
