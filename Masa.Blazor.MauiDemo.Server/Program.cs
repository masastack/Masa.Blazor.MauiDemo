using Masa.Blazor.MauiDemo.Platforms;
using Masa.Blazor.MauiDemo.Rcl.Data;
using Masa.Blazor.MauiDemo.Rcl.Services;
using Masa.Blazor.MauiDemo.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMasaBlazorMauiDemo();
builder.Services.AddSingleton<ProDatabase>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IPlatformIntegration, H5PlatformIntegration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
