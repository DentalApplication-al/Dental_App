﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientDesccriptionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Clients");
        }
    }
}
