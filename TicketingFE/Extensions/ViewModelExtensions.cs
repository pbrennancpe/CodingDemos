using TicketingFE.Models;

namespace TicketingFE.Extensions
{

    public static class ViewModelExtensions
    {
        public static TicketViewModel ToViewModel(this TicketResponseDTO ticketDTO)
        {
            return new TicketViewModel
            {
                Id = ticketDTO.Id,
                Title = ticketDTO.Title,
                TicketNo = ticketDTO.TicketNo,
                Description = ticketDTO.Description,
                TicketStatus = ticketDTO.TicketStatus,
                CreatedAt = ticketDTO.CreatedAt,
                UpdatedAt = ticketDTO.UpdatedAt,
                AssignedUser = ticketDTO.AssignedUser == null
                    ? null : ticketDTO.AssignedUser.ToViewModel()
            };

        }
        public static UserViewModel ToViewModel(this UserDTO userDTO)
        {
            return new UserViewModel
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Email = userDTO.Email
            };
        }





    }
}