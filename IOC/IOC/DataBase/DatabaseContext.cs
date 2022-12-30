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
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CeaiCuColegii;Trusted_Connection=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IDUser).HasName("PK_User");

                entity.ToTable("User");

                entity.Property(e => e.IDUser).HasColumnName("IDUser");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("surname");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");
                entity.Property(e => e.MailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MailAddress");
                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });
        }

        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
