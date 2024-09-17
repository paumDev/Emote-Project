using BethanysPieShopHRM.App;
using Blazored.LocalStorage;
using BethanysPieShopHRM.App.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IEmoteDataService, EmoteDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<IUserDataService, UserDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped<ImageDownloader>();
builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
