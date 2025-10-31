using Microsoft.AspNetCore.Mvc;
using Ticketing;
using Ticketing.Data;
using Ticketing.Models.DTO;
using Ticketing.Models;
using Ticketing.Services.Interfaces;


namespace Ticketing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController(ITicketService service) : ControllerBase
    {

        public async Task<IActionResult> GetTickets()
        {
            try
            {
                var tickets = await service.GetTickets(new TicketQuery());

                if (tickets != null)
                {
                    return Ok(tickets);
                }
                else return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets([FromQuery] TicketQuery query)
        {
            try
            {
                var tickets = await service.GetTickets(query);

                if (tickets != null)
                {
                    return Ok(tickets);
                }
                else return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetByID(Guid id)
        {
            try
            {
                var ticket = await service.GetTicketById(id);

                if (ticket != null)
                {
                    return Ok(ticket);
                }
                else return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketRequestDTO requestDTO)
        {   
            try
            {
                var result = await service.CreateTicket(requestDTO);

                if (result == true)
                {
                    return Ok();
                }
                else return StatusCode(500);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTicket(UpdateTicketDTO requestDTO)
        {
            try
            {
                var result = await service.UpdateTicket(requestDTO);

                if (result == true)
                {
                    return Ok();
                }
                else return StatusCode(500);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        
        
        //TO DO: Possibly add an extra method to assign a user just for ease of use
    }


}