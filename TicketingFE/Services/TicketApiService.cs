using System.Net.Http.Json;
using TicketingFE.Models;

public class TicketApiService(HttpClient client)
{

    public async Task<List<TicketResponseDTO>?> GetTicketsAsync(TicketQuery? query = null)
    {
        string url = "Tickets";

        // Build query string manually if filters are provided
        if (query != null)
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrWhiteSpace(query.Status))
                queryParams.Add($"status={Uri.EscapeDataString(query.Status)}");
            // Only add UserId if it's a valid Guid string
            if (!string.IsNullOrWhiteSpace(query.UserId) &&
                Guid.TryParse(query.UserId, out Guid parsedUserId))
            {
                queryParams.Add($"userId={parsedUserId}");
            }

            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);
        }

        return await client.GetFromJsonAsync<List<TicketResponseDTO>>(url);
    }

    public async Task<List<TicketResponseDTO>> GetTicketsAsync()
    {
        return await client.GetFromJsonAsync<List<TicketResponseDTO>>("endpoint");
    }

    public async Task<TicketResponseDTO?> GetTicketByIdAsync(Guid id)
    {
        return await client.GetFromJsonAsync<TicketResponseDTO>($"Tickets/{id}");
    }

    // POST /Tickets (create)
    public async Task<bool> CreateTicketAsync(TicketRequestDTO dto)
    {
        var response = await client.PostAsJsonAsync("Tickets", dto);
        return response.IsSuccessStatusCode;
    }

    // POST /Tickets (update)
    public async Task<bool> UpdateTicketAsync(UpdateTicketDTO dto)
    {
        var response = await client.PostAsJsonAsync("Tickets", dto);
        return response.IsSuccessStatusCode;
    }
}   