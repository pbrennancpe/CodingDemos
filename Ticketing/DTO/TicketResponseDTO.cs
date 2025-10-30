using Ticketing.Models.Enums;

namespace Ticketing.DTO
{
    public class TicketResponseDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string TicketStatus { get; set; } = "Open";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDTO? AssignedUser { get; set; }
    }
}