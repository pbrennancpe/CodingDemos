using Ticketing;
using Ticketing.Models.DTO;

namespace Ticketing.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketResponseDTO>> GetTickets(TicketQuery query);
        Task<TicketResponseDTO> GetTicketById(Guid id);
        Task<bool> UpdateTicket(Guid ticketId, TicketRequestDTO request);
        Task<bool> CreateTicket(TicketRequestDTO request);

    }
}