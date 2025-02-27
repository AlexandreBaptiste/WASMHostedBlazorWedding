using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Wedding.Client;
using Wedding.Interfaces;
using Wedding.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddBlazoredSessionStorage();
builder.Logging.SetMinimumLevel(LogLevel.None);

await builder.Build().RunAsync();
