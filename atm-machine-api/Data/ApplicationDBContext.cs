using Microsoft.EntityFrameworkCore;
using atm_machine_api.Models;

namespace atm_machine_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
        : base(dbContextOptions)
        { }

        public DbSet<Users> Users { get; set; } = null;
        public DbSet<UsersTransactionHistory> UsersTransactionHistories { get; set; } = null;
    }
}