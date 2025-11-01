using TicketingFE.Extensions;

namespace TicketingFE.Models
{
    public class TicketRequestDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? AssignedUserId { get; set; }
    }

    public class UpdateTicketDTO : TicketRequestDTO
    {
        public required Guid Id { get; set; }
        public string? TicketStatus { get; set; }
    }
}