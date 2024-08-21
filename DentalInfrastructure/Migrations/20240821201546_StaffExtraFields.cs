using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StaffExtraFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "Staffs",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "Staffs",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Staffs");
        }
    }
}
