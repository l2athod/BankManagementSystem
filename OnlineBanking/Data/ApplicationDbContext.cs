using Microsoft.EntityFrameworkCore;
using OnlineBanking.Models;

namespace OnlineBanking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { } 
        public DbSet<OnlineBanking.Models.Login> Login { get; set; }
        public DbSet<OnlineBanking.Models.Customer> Customer { get; set; }
        public DbSet<OnlineBanking.Models.TransactionModel> Transaction { get; set; }
        public DbSet<OnlineBanking.Models.AccountTransaction> AccountTransaction { get; set; }

    }
}