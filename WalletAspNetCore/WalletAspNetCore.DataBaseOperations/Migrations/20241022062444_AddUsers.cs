using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletAspNetCore.DataBaseOperations.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FK_Transactions_User_UserId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FK_Balance_User_UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Balances_FK_Balance_User_UserId",
                        column: x => x.FK_Balance_User_UserId,
                        principalTable: "Balances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FK_Transactions_User_UserId",
                table: "Transactions",
                column: "FK_Transactions_User_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_FK_Balance_User_UserId",
                table: "User",
                column: "FK_Balance_User_UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_User_FK_Transactions_User_UserId",
                table: "Transactions",
                column: "FK_Transactions_User_UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_User_FK_Transactions_User_UserId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FK_Transactions_User_UserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FK_Transactions_User_UserId",
                table: "Transactions");
        }
    }
}
