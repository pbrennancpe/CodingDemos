using System;
using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models
{

    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }

        public IList<Ticket> Tickets {get; set;} = new List<Ticket>();



    }



}