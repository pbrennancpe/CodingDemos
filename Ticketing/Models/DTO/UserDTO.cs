using Ticketing.Models.Enums;

namespace Ticketing.Models.DTO
{
    public class UserDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
    }
}