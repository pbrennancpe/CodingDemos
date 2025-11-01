using System.Net.Http.Json;
using TicketingFE.Models;

public class UserApiService(HttpClient client)
{

    public async Task<List<UserDTO>?> GetUsersAsync()
    {

        var users = await client.GetFromJsonAsync<List<UserDTO>>("Users");

        return users?.OrderBy(x => x.Name).ToList();
    }
}   