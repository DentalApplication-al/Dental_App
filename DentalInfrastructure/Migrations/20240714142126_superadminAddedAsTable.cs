using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class superadminAddedAsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: new Guid("87732c86-7bb7-44c8-90bd-19ea4b841800"));

            migrationBuilder.CreateTable(
                name: "SuperAdmin",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdmin", x => x.Username);
                });

            migrationBuilder.InsertData(
                table: "SuperAdmin",
                columns: new[] { "Username", "Password" },
                values: new object[] { "SuperAdmin", "password" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperAdmin");

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "Birthday", "CreatedOn", "Email", "FirstName", "LastName", "Password", "Phone", "Role", "UpdatedOn", "Username" },
                values: new object[] { new Guid("87732c86-7bb7-44c8-90bd-19ea4b841800"), new DateTime(2024, 7, 13, 20, 41, 32, 824, DateTimeKind.Utc).AddTicks(8794), new DateTime(2024, 7, 13, 20, 41, 32, 824, DateTimeKind.Utc).AddTicks(8914), "admin@gmail.com", "Super", "Admin", "Password1.", "06868688", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin" });
        }
    }
}
