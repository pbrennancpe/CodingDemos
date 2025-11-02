using TicketingFE.Extensions;

namespace TicketingFE.Models
{
    public class TicketRequestDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
    //Empty class for easier naming
    public class TicketCreateDTO : TicketRequestDTO
    {
        
    }

    public class TicketUpdateDTO : TicketRequestDTO
    {
        public required Guid Id { get; set; }
        public Status? TicketStatus { get; set; }
    }
}