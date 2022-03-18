using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atm_machine_api.Migrations
{
    public partial class addColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "typeOfTransaction",
                table: "UsersTransactionHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "typeOfTransaction",
                table: "UsersTransactionHistories");
        }
    }
}
