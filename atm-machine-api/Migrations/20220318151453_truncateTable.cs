using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atm_machine_api.Migrations
{
    public partial class truncateTable : Migration
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
