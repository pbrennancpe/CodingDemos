using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using TicketingFE;
using System.Text.Json;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddScoped<TicketApiService>();
builder.Services.AddScoped<UserApiService>();



var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await http.GetFromJsonAsync<Dictionary<string, object>>("appsettings.json");

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

await builder.Build().RunAsync();
