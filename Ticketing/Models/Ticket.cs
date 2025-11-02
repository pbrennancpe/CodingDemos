using System;
using Ticketing.Models.Enums;


namespace Ticketing.Models
{

    public class Ticket
    {
        public Guid Id { get; set; }
        public int TicketNo { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Status TicketStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User? AssignedUser { get; set; }
        public Guid? AssignedUserId { get; set; }

    }

}