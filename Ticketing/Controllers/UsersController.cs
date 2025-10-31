using Microsoft.AspNetCore.Mvc;
using Ticketing;
using Ticketing.Data;
using Ticketing.Models.DTO;
using Ticketing.Models;
using Ticketing.Services.Interfaces;


namespace Ticketing.Controllers
{

    //Ideally this method and controller would be in their own UsersService
    //If I have time I will break this out into it's own service at the end but at the moment
    //It only exists to populate the "Assign User" drop down on the UI
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService service) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await service.GetUsers();

                return Ok(users);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

 
        
        
    }


}