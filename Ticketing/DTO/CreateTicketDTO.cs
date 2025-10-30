using Ticketing.Models.Enums;

namespace Ticketing.DTO
{
    public class CreateTicketDTO
    {
        public required string Title { get; set; }
        public string? Email { get; set; }
        public required string TicketStatus { get; set; } = "Open";
        public string? AssignedUserName { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
}