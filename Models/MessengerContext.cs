using Microsoft.EntityFrameworkCore;

namespace MessengerApplication.Models
{
    public partial class MessengerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        private string _connectionString;

        //public MessengerContext()
        //{
        //    Database.EnsureCreated();
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=9150;Database=PostgreSQL16;Username=postgres;Password=9150")
        //.UseLazyLoadingProxies();
        //}
        public MessengerContext()
        {
        }
        public MessengerContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);

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
    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
