using System.Data;
using Npgsql;
using TradingAppMvc.Application.Features;
using TradingAppMvc.Services;
using TradingAppMvc.Repositories;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Clients;
using Microsoft.AspNetCore.Identity;
using TradingAppMvc.Repositories.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddSingleton<IBinanceRestClient, BinanceRestClient>();
builder.Services.AddSingleton<IBinanceSocketClient, BinanceSocketClient>();
builder.Services.AddSingleton<IBinanceAccountProvider, BinanceAccountProvider>();
builder.Services.AddDbContext<TradingAppMvc.Repositories.Identity.IdentityDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<TradingAppMvc.Repositories.Identity.IdentityDbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();
builder.Services.AddScoped<IDbConnection>(provider => new NpgsqlConnection(builder.Configuration.GetConnectionString("ApplicationConnection")));
// builder.Services.AddMemoryCache();
// builder.Services.AddBinance();
// builder.Services.AddSingleton<IBinanceRestClient, BinanceRestClient>();
// builder.Services.AddSession(options =>
// {
//     options.Cookie.Name = ".TradingApp.Session";
//     options.IdleTimeout = TimeSpan.FromMinutes(10);
//     options.Cookie.IsEssential = true;
// });
// builder.Services.AddDistributedMemoryCache();

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
// app.UseSession();
app.UseCookiePolicy();
app.UseAuthorization();
app.MapControllers();

app.Run();
