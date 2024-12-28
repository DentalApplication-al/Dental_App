using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Appointments_AppointmentId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Clients_ClientId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_ClientId",
                table: "Files",
                newName: "IX_Files_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_AppointmentId",
                table: "Files",
                newName: "IX_Files_AppointmentId");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "Files",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Appointments_AppointmentId",
                table: "Files",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Clients_ClientId",
                table: "Files",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Appointments_AppointmentId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Clients_ClientId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ClientId",
                table: "Documents",
                newName: "IX_Documents_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_AppointmentId",
                table: "Documents",
                newName: "IX_Documents_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Appointments_AppointmentId",
                table: "Documents",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Clients_ClientId",
                table: "Documents",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
