using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atm_machine_api.Migrations
{
    public partial class addForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Usersid",
                table: "UserTransactionHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserTransactionHistories_Usersid",
                table: "UserTransactionHistories",
                column: "Usersid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTransactionHistories_Users_Usersid",
                table: "UserTransactionHistories",
                column: "Usersid",
                principalTable: "Users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTransactionHistories_Users_Usersid",
                table: "UserTransactionHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserTransactionHistories_Usersid",
                table: "UserTransactionHistories");

            migrationBuilder.DropColumn(
                name: "Usersid",
                table: "UserTransactionHistories");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Users");
        }
    }
}
