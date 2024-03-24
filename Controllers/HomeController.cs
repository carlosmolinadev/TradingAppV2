using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Controllers;

[Route("[controller]")]
[ApiController]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("~/")]
    [Route("index")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("welcome")]
    public IActionResult GetWelcomeComponent()
    {
        var order = new TradeOrder(){Symbol = "USDT"};
        //return new RazorComponentResult<Welcome>(new { Message = "Hello, world!" });
        return View();
    }
}
