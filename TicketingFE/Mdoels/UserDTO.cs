namespace TicketingFE.Models
{
    public class UserDTO
    {
        public required Guid? Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
    }
}