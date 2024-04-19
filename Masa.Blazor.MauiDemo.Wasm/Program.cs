using Masa.Blazor.MauiDemo.Platforms;
using Masa.Blazor.MauiDemo.Rcl.Data;
using Masa.Blazor.MauiDemo.Rcl.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Masa.Blazor.MauiDemo.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMasaBlazorMauiDemo();
builder.Services.AddSingleton(_ = new ProDatabase());
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IPlatformIntegration, H5PlatformIntegration>();

await builder.Build().RunAsync();