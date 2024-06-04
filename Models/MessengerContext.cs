using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MessengerApplication.Models
{
    public partial class MessengerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        //private string _connectionString;

        //public MessengerContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseNpgsql(_connectionString);

        //public MessengerContext()
        //{
        //    Database.EnsureCreated();
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=9150;Database=PostgreSQL16");
        //.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("user_pkey");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255);
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Salt).HasColumnName("salt");
                entity.Property(e => e.RoleId).HasConversion<int>();
                //entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(255);
                //entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(255);
                //entity.Property(e => e.Registered).HasColumnName("registered");
                //entity.Property(e => e.Active).HasColumnName("active");
            });

            modelBuilder
                .Entity<Role>()
                .Property(e => e.RoleId)
                .HasConversion<int>();

            modelBuilder
                .Entity<Role>()
                .HasData(Enum.GetValues(typeof(RoleId))
                .Cast<RoleId>()
                .Select(e => new Role()
                {
                    RoleId = e,
                    Email = e.ToString()
                }));

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
