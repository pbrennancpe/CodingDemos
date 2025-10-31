using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ticketing;
using Ticketing.Data;
using Ticketing.Models.DTO;
using Ticketing.Extensions;
using Ticketing.Models;
using Ticketing.Services.Interfaces;

namespace Ticketing.Services 
{
    public class UserService(TicketingDBContext context) : IUserService
    {

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = context.Users.AsQueryable();

            if (users == null)
            {
                //Throw an exception
                throw new Exception();
            }
            

            return await users.Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email

            }).ToListAsync();
        }
    }
    
}