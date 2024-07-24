using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: new Guid("dd110d6a-e10b-4dcb-bb69-537e2ad9dfa5"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Staffs");

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "Birthday", "CreatedOn", "Email", "FirstName", "LastName", "Password", "Phone", "UpdatedOn", "Username" },
                values: new object[] { new Guid("0be364fb-16b6-4e16-95cb-7f2c08a74a14"), new DateTime(2024, 7, 13, 20, 40, 21, 568, DateTimeKind.Utc).AddTicks(1364), new DateTime(2024, 7, 13, 20, 40, 21, 568, DateTimeKind.Utc).AddTicks(1463), "admin@gmail.com", "Super", "Admin", "Password1.", "06868688", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: new Guid("0be364fb-16b6-4e16-95cb-7f2c08a74a14"));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "Birthday", "CreatedOn", "Email", "FirstName", "LastName", "Password", "Phone", "Role", "UpdatedOn", "Username" },
                values: new object[] { new Guid("dd110d6a-e10b-4dcb-bb69-537e2ad9dfa5"), new DateTime(2024, 7, 13, 20, 11, 39, 736, DateTimeKind.Utc).AddTicks(3799), new DateTime(2024, 7, 13, 20, 11, 39, 736, DateTimeKind.Utc).AddTicks(3909), "admin@gmail.com", "Super", "Admin", "Password1.", "06868688", "SUPERADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin" });
        }
    }
}
