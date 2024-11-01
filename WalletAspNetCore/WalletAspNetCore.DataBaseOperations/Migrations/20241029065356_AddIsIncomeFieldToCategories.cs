﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletAspNetCore.DataBaseOperations.Migrations
{
    /// <inheritdoc />
    public partial class AddIsIncomeFieldToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "Categories",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "Categories");
        }
    }
}
