using System;
using System.ComponentModel.DataAnnotations;
using Ticketing.Models;
using Ticketing.Models.DTO;

namespace Ticketing.Extensions
{

    public static class TicketExtensions
    {
        public static TicketResponseDTO ToResponseDTO(this Ticket value)
        {
            return new TicketResponseDTO
            {
                Id = value.Id,
                TicketNo = value.TicketNo,
                Title = value.Title,
                Description = value.Description,
                CreatedAt = value.CreatedAt,
                UpdatedAt = value.UpdatedAt,
                TicketStatus = value.TicketStatus,
                AssignedUser = value.AssignedUser != null && value.AssignedUserId != null ?
                 new UserDTO
                 {
                     Id = value.AssignedUser.Id,
                     Name = value.AssignedUser.Name,
                     Email = value.AssignedUser.Email
                 }
                 : null
            };
        }
    }



}