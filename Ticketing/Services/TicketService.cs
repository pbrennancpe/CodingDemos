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
    public class TicketService : ITicketService
    {
        private readonly TicketingDBContext _context;

        public TicketService(TicketingDBContext context)
        {
            _context = context;
        }
        public async Task<TicketResponseDTO> GetTicketById(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                //Throw an exception
                throw new Exception();
            }
            

            return new TicketResponseDTO
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt,
                TicketStatus = ticket.TicketStatus,
                AssignedUser = ticket.AssignedUser != null && ticket.AssignedUserId != null ?
                 new UserDTO
                 {
                     Id = ticket.AssignedUser.Id,
                     Name = ticket.AssignedUser.Name,
                     Email = ticket.AssignedUser.Email
                 }
                : null
            };
        }

        public async Task<IEnumerable<TicketResponseDTO>> GetTickets(TicketQuery query)
        {
            var tickets = _context.Tickets.AsQueryable();

            if (string.IsNullOrEmpty(query?.Status))
            {
                var status = query?.Status?.ToStatusEnum();

                tickets = tickets.Where(x => x.TicketStatus == status);
            }
            if(query?.User.HasValue == true)
            {
                tickets = tickets.Where(x => x.AssignedUserId == query.User);
            }
            return await tickets.Select(x => new TicketResponseDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                TicketStatus = x.TicketStatus,
                AssignedUser = x.AssignedUser != null && x.AssignedUserId != null ?
                 new UserDTO
                 {
                     Id = x.AssignedUser.Id,
                     Name = x.AssignedUser.Name,
                     Email = x.AssignedUser.Email
                 }
                 : null
            }).ToListAsync();
        }
        public async Task<bool> UpdateTicket(Guid ticketId, TicketRequestDTO request)
        {
            bool result = true;
            var ticket = await _context.Tickets.FindAsync(ticketId);

            //Ideally this would be handled with reflection in a helper method.
            //If I feel like I have more time I can make that enhancement but it won't actually
            //Save much time at the moment since we are only updating one object
            if (ticket == null)
                return false;

            ticket.AssignedUserId = request.AssignedUserId != null &&
                await _context.Users.FindAsync(request.AssignedUserId) != null ?
                 request.AssignedUserId :
                 ticket.AssignedUserId;

            ticket.Title = request.Title ?? ticket.Title;
            ticket.Description = request.Description ?? ticket.Description;
            ticket.TicketStatus = request.TicketStatus != null ?
                request.TicketStatus.ToStatusEnum() :
                ticket.TicketStatus;

        
            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            

            return result;
        }
        public async Task<bool> CreateTicket(TicketRequestDTO request)
        {
            //Only assign the FK if that guid exists
            var userId = request.AssignedUserId;
            if (userId != null)
            {
                userId = await _context.Users.FindAsync(userId) != null ? userId : null;
            }

            Ticket newTicket = new Ticket
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                TicketStatus = Models.Enums.Status.Open,
                AssignedUserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Tickets.Add(newTicket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}