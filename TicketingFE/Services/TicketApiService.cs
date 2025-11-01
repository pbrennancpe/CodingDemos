using System.Net.Http.Json;
using Microsoft.JSInterop;
using TicketingFE.Models;
using TicketingFE.Helpers;


public class TicketApiService(HttpClient client, IJSRuntime js)
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
        var fullUrl = new Uri(client.BaseAddress ?? new Uri(""), url);
        _ = LogHelper.DebugLogAsync(js, $"Full request URL: {fullUrl}");

        List<TicketResponseDTO> tickets = null;
    try
    {
        var response = await client.GetAsync(url);

        // Log HTTP status and headers
        _ = LogHelper.DebugLogAsync(js, $"HTTP Status: {response.StatusCode}");
        _ = LogHelper.DebugLogAsync(js, $"Reason: {response.ReasonPhrase}");

        response.EnsureSuccessStatusCode(); // Throws if not 2xx

        tickets = await response.Content.ReadFromJsonAsync<List<TicketResponseDTO>>();
        return tickets;
    }
        catch(Exception ex)
        {
            _ = LogHelper.DebugLogAsync(js, $"Exception: {ex.Message}");
        }
        return tickets;
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