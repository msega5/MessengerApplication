using MessengerApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace MessengerApplication.Context
{
    public class MessengerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Message> Messages { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=9150;Database=PostgreSQL16;Username=postgres;Password=9150");
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

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");
                entity.HasKey(x => x.MessageId).HasName("messagePk");

                entity.Property(e => e.Text).HasColumnName("messageText");
                entity.Property(e => e.DateSend).HasColumnName("messageData");
                entity.Property(e => e.IsSent).HasColumnName("is_sent");
                entity.Property(e => e.MessageId).HasColumnName("id");

                entity
                    .HasOne(x => x.UserTo)
                    .WithMany(m => m.MessageTo)
                    .HasForeignKey(x => x.UserToId)
                    .HasConstraintName("messageToUserFK");

                entity
                    .HasOne(x => x.UserFrom)
                    .WithMany(m => m.MessageFrom)
                    .HasForeignKey(x => x.UserFromId)
                    .HasConstraintName("messageFromUserFK");
            });
        }
    }
}
