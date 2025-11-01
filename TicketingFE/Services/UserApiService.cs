using System.Net.Http.Json;
using TicketingFE.Models;

public class UserApiService(HttpClient client)
{

    public async Task<List<UserDTO>?> GetUsersAsync()
    {
        return await client.GetFromJsonAsync<List<UserDTO>>("Users");
    }
}   