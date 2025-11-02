using Ticketing;
using Ticketing.Models.DTO;

namespace Ticketing.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketResponseDTO>> GetTickets(TicketQuery query);
        Task<TicketResponseDTO> GetTicketByNo(int ticketNo);
        Task<TicketResponseDTO> GetTicketById(Guid id);
        Task<TicketResponseDTO> UpdateTicket(UpdateTicketDTO request);
        Task<TicketResponseDTO> CreateTicket(TicketRequestDTO request);

    }
}