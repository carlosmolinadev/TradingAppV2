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
    public IActionResult Index()
    {
        if (User.Identity != null && !User.Identity.IsAuthenticated)
        {
            return View("Login");
        }
        return View();
    }
}
