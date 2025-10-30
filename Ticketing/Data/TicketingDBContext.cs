using Microsoft.EntityFrameworkCore;
using Ticketing.Models;

namespace Ticketing.Data
{
    public class TicketingDBContext : DbContext
    {
        public TicketingDBContext(DbContextOptions<TicketingDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(x => x.AssignedUser)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.AssignedUserId)
                .IsRequired(false);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    Name = "Paul Brennan",
                    Email = "programer@asdf.com"
                },

                new User
                {
                    Id = Guid.Parse("9a1b2c3d-4e5f-6789-abcd-1234567890ef"),
                    Name = "Steve Rogers",
                    Email = "captainamerica@asdf.com"
                },

                new User
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "Bruce Wayne",
                    Email = "batman@asdf.com"
                },

                new User
                {
                    Id = Guid.Parse("6b29fc40-ca47-1067-b31d-00dd010662da"),
                    Name = "Wanda Maximoff",
                    Email = "scarletwitch@asdf.com"
                },

                new User
                {
                    Id = Guid.Parse("e02fd0e4-00fd-090A-ca30-0d00a0038ba0"),
                    Name = "Scott Summers",
                    Email = "cyclops@asdf.com"
                }
            );
        }

    }
}
