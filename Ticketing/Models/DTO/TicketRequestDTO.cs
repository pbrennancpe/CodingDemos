using Ticketing.Models.Enums;

namespace Ticketing.Models.DTO
{
    public class TicketRequestDTO
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string TicketStatus { get; set; } = "Open";
        public Guid? AssignedUserId { get; set; }
    }
}