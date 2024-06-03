using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Reflection.Emit;

namespace MessengerApplication.Models
{
    public class MessengerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //private string _connectingString;

        public MessengerContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=gb;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=True;")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("user_id");
                entity.HasIndex(e => e.Email).IsUnique();

                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(255);
                entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(255);
                entity.Property(e => e.Registered).HasColumnName("registered");
                entity.Property(e => e.Active).HasColumnName("active");
                entity.Property(e => e.Password).HasColumnName("password");
            });

            //OnModelCreatingPartial(modelBuilder);
        }
    }
   // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
