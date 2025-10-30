using Ticketing;
using Ticketing.DTO;
using Ticketing.Services.Interfaces;

namespace Ticketing.Services 
{
    public class TicketingService : ITicketService
    {
        public IEnumerable<TicketResponseDTO> GetTickets(TicketQuery query)
        {
            return null;
        }
        public bool UpdateTicket(Guid ticketId, TicketRequestDTO request)
        {
            return true;
        }
        public bool CreateTicket(TicketRequestDTO request)
        {
            return true;
        }
    }
    
}