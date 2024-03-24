using System.Data;
using System.Data.Common;
using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using Npgsql;
using TradingAppMvc.Application.Features;
using TradingAppMvc.Application.Interfaces.Services;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Domain.Repositories;
using TradingAppMvc.Infraestructure.Repositories;
using TradingAppMvc.Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddMemoryCache();
builder.Services.AddBinance();
// builder.Services.AddSingleton<IBinanceRestClient, BinanceRestClient>();
builder.Services.AddScoped<IDbConnection>(provider => new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=trading;UserId=trading;Password=trading;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
