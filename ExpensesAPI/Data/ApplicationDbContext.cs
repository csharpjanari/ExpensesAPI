
namespace ExpensesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}
