using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletAspNetCore.DataBaseOperations.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_User_FK_Transactions_User_UserId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Balances_FK_Balance_User_UserId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_FK_Balance_User_UserId",
                table: "Users",
                newName: "IX_Users_FK_Balance_User_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_FK_Transactions_User_UserId",
                table: "Transactions",
                column: "FK_Transactions_User_UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Balances_FK_Balance_User_UserId",
                table: "Users",
                column: "FK_Balance_User_UserId",
                principalTable: "Balances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_FK_Transactions_User_UserId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Balances_FK_Balance_User_UserId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FK_Balance_User_UserId",
                table: "User",
                newName: "IX_User_FK_Balance_User_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_User_FK_Transactions_User_UserId",
                table: "Transactions",
                column: "FK_Transactions_User_UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Balances_FK_Balance_User_UserId",
                table: "User",
                column: "FK_Balance_User_UserId",
                principalTable: "Balances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
