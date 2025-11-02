using TicketingFE.Extensions;
namespace TicketingFE.Models
{
    public class TicketViewModel
    {
        public Guid? Id { get; set; }
        public int? TicketNo { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TicketStatusDisplay
        {
            get
            {
                return TicketStatus.GetDisplayName();
            }
        }
        public Status? TicketStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserViewModel? AssignedUser { get; set; }
    }
}