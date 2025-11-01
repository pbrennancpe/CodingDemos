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
    public class TicketService(TicketingDBContext context) : ITicketService
    {

        public async Task<TicketResponseDTO> GetTicketById(Guid id)
        {
            var ticket = await context.Tickets
                .Include(x => x.AssignedUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
            {
                //Don't throw an exception just return null
                return null;
            }
            

            return ticket.ToResponseDTO();
        }

        public async Task<IEnumerable<TicketResponseDTO>> GetTickets(TicketQuery query)
        {
            var tickets = context
                .Tickets.Include(x => x.AssignedUser)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(query?.Status))
            {
                var status = query?.Status?.ToStatusEnum();

                tickets = tickets.Where(x => x.TicketStatus == status);
            }
            if(query?.UserId.HasValue == true)
            {
                tickets = tickets.Where(x => x.AssignedUserId == query.UserId);
            }
            return await tickets.Select(x => x.ToResponseDTO()).ToListAsync();
        }
        public async Task<TicketResponseDTO> UpdateTicket(UpdateTicketDTO request)
        {
            var ticket = await context.Tickets
                .Include(x => x.AssignedUser)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (ticket == null)
                return null;

            ticket.AssignedUserId = request.AssignedUserId != null &&
                await context.Users.FindAsync(request.AssignedUserId) != null ?
                 request.AssignedUserId :
                 ticket.AssignedUserId;

            ticket.Title = request.Title ?? ticket.Title;
            ticket.Description = request.Description ?? ticket.Description;
            ticket.TicketStatus = request.TicketStatus != null &&
            request.TicketStatus.ToStatusEnum() != Models.Enums.Status.Error ?
                request.TicketStatus.ToStatusEnum() :
                ticket.TicketStatus;

        
            ticket.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            

            return ticket.ToResponseDTO();
        }
        public async Task<TicketResponseDTO> CreateTicket(TicketRequestDTO request)
        {
            //Only assign the FK if that guid exists
            var userId = request.AssignedUserId;
            if (userId != null)
            {
                userId = await context.Users.FindAsync(userId) != null ? userId : null;
            }

            Ticket newTicket = new Ticket
            {
                Id = Guid.NewGuid(),
                Title = request.Title ?? "New Ticket",
                Description = request.Description,
                TicketStatus = Models.Enums.Status.Open,
                AssignedUserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Tickets.Add(newTicket);
            await context.SaveChangesAsync();
            return newTicket.ToResponseDTO();
        }
    }
    
}