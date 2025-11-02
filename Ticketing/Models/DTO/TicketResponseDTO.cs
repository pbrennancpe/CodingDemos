using Microsoft.OpenApi.Extensions;
using Ticketing.Models.Enums;

namespace Ticketing.Models.DTO
{
    public class TicketResponseDTO
    {
        public Guid Id { get; set; }
        public int? TicketNo { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? TicketStatusDisplay
        {
            get
            {
                return TicketStatus.GetDisplayName();
            }
        }
        public Status TicketStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDTO? AssignedUser { get; set; }
    }
}