using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace TicketingFE.Helpers
{
    public static class LogHelper
    {
        public static async Task DebugLogAsync(IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("console.log", message);
        }
        public static async Task DebugLogObjectAsync( IJSRuntime js, object obj)
        {
            var jsonContent = JsonContent.Create(obj);
            var jsonString = await jsonContent.ReadAsStringAsync();


            await js.InvokeVoidAsync("console.log", jsonString);
        }
    }
}