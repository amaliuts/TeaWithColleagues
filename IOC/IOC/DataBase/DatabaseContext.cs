using IOC.Models;
using Microsoft.EntityFrameworkCore;

namespace IOC.DataBase
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost;;Database=CeaiCuColegii;Trusted_Connection=true;TrustServerCertificate=true");

        }

        public DbSet<Availability> Availabilities { get; set; }
    }
}
