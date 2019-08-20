using Microsoft.EntityFrameworkCore;
using EmailSender.Models;

namespace EmailSender
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }
        public readonly string connectionString;

        public ApplicationContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Email>()
                .HasMany(e => e.Attachments)
                .WithOne(a => a.Email);
            modelBuilder.Entity<EmailAttachment>()
                .HasKey(a => a.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
