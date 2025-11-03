using Microsoft.EntityFrameworkCore;
using Ticketing.Models;
using Ticketing.Models.Enums;

namespace Ticketing.Data
{
    public class TicketingDBContext : DbContext
    {
        public TicketingDBContext(DbContextOptions<TicketingDBContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasSequence<int>("TicketNumbers", schema: "shared")
                .StartsAt(1)
                .IncrementsBy(1);

             modelBuilder.Entity<Ticket>()
                .Property(o => o.TicketNo)
                .HasDefaultValueSql("NEXT VALUE FOR shared.TicketNumbers");

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

            modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = Guid.Parse("b6a5f2c8-9e44-4c1b-8f0d-2f8f65a7a3c1"),
                Title = "Fix login issue",
                Description = "Users cannot log in after latest update",
                TicketStatus = Status.Open,
                CreatedAt = new DateTime(2025, 11, 3, 10, 0, 0),
                UpdatedAt = new DateTime(2025, 11, 3, 10, 0, 0),
                AssignedUserId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479") 
            },
            new Ticket
            {
                Id = Guid.Parse("d3e2c5f1-7c4b-4b8d-83e0-bb7d5cbb4f11"),
                Title = "Update user profile page",
                Description = "Redesign profile page for better UX",
                TicketStatus = Status.InProgress,
                CreatedAt = new DateTime(2025, 11, 3, 11, 0, 0),
                UpdatedAt = new DateTime(2025, 11, 3, 11, 0, 0),
                AssignedUserId = Guid.Parse("9a1b2c3d-4e5f-6789-abcd-1234567890ef") 
            },
            new Ticket
            {
                Id = Guid.Parse("f8c4d7a2-1b2c-4d3f-a5e6-9f0b4c1d2e3f"),
                Title = "Fix payment gateway",
                Description = "Payment failures reported on checkout",
                TicketStatus = Status.Closed,
                CreatedAt = new DateTime(2025, 11, 3, 12, 0, 0),
                UpdatedAt = new DateTime(2025, 11, 3, 12, 0, 0),
                AssignedUserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6") 
            },
            new Ticket
            {
                Id = Guid.Parse("c9a1b2d3-4e5f-6789-abcd-0f1e2d3c4b5a"),
                Title = "Server maintenance",
                Description = "Schedule downtime for server upgrades",
                TicketStatus = Status.Closed,
                CreatedAt = new DateTime(2025, 11, 3, 13, 0, 0),
                UpdatedAt = new DateTime(2025, 11, 3, 13, 0, 0),
                AssignedUserId = Guid.Parse("6b29fc40-ca47-1067-b31d-00dd010662da") 
            },
            new Ticket
            {
                Id = Guid.Parse("e1f2d3c4-5b6a-7c8d-9e0f-1a2b3c4d5e6f"),
                Title = "Email notification bug",
                Description = "Some users are not receiving emails",
                TicketStatus = Status.Open,
                CreatedAt = new DateTime(2025, 11, 3, 14, 0, 0),
                UpdatedAt = new DateTime(2025, 11, 3, 14, 0, 0),
                AssignedUserId = null 
            }
        );
        }

    }
}
