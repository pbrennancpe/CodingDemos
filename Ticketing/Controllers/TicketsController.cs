using Microsoft.AspNetCore.Mvc;
using Ticketing;
using Ticketing.Data;
using Ticketing.DTO;
using Ticketing.Models;


namespace Ticketing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetTickets([FromQuery] TicketQuery query)
        {


            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateTicket(TicketRequestDTO requestDTO)
        {

            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateTicket(Guid id, [FromBody] TicketRequestDTO requestDTO)
        {

            return Ok();
        }
        
        //TO DO: Possibly add an extra method to assign a user just for ease of use
    }


}