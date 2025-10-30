using Ticketing.Models.Enums;

namespace Ticketing.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
    }
}