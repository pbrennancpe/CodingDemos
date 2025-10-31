using Ticketing;
using Ticketing.Models.DTO;

namespace Ticketing.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();

    }
}