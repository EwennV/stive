﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StiveBack.Migrations
{
    /// <inheritdoc />
    public partial class AjoutdelattributIsAdminàlutilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "users");
        }
    }
}
