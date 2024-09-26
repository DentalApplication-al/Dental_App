using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientOtherFieldsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BleedingDisorders",
                table: "Clients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentMedications",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Diabetes",
                table: "Clients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HeartCondition",
                table: "Clients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hypertension",
                table: "Clients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Immunocompromised",
                table: "Clients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherConditions",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialNotes",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BleedingDisorders",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CurrentMedications",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Diabetes",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HeartCondition",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Hypertension",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Immunocompromised",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "OtherConditions",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SpecialNotes",
                table: "Clients");
        }
    }
}
