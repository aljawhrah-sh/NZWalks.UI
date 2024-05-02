using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NZWalks.UI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<NZWalksServiceSettings>(
    builder.Configuration.GetSection("NZWalksServiceSettings"));


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<NZWalksService>();
builder.Services.AddScoped<NZWalksTypedClientService>();

//builder.Services.AddHttpClient<NZWalksTypedClientService>((serviceProvider, client) =>
//{
//    var settings = serviceProvider.GetRequiredService<IOptions<NZWalksServiceSettings>>().Value;
//    client.BaseAddress = new Uri("https://localhost:7100/api/");
//});

builder.Services.AddHttpClient("NZWalks", (serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<NZWalksServiceSettings>>().Value;


    client.BaseAddress = new Uri("https://localhost:7100/api/");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

