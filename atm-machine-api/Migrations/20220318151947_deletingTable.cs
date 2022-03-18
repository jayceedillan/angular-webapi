using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atm_machine_api.Migrations
{
    public partial class deletingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("truncate table UserTransactionHistories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
