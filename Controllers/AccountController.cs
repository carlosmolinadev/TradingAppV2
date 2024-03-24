using Microsoft.AspNetCore.Mvc;
using TradingAppMvc.Application.Interfaces.Services;
using TradingAppMvc.Domain.Entities;

namespace TradingAppMvc.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<HomeController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet("exchange/{accountId}")]
    public IActionResult GetExchangeByAccount()
    {
        return View();
    }

    [HttpGet("exchange")]
    public async Task<IActionResult> GetFuturesBalance()
    {
        var account = new Account(new Guid("7225d9f0-3f42-4705-ae07-a36acaf19a93"),  1, 1, new Guid("c96666c1-2ed3-4888-b775-9f4ebe277bed"));
        var response = await _accountService.GetFuturesBalance(account);
        if(response.Success){
            account = response.Result;
        }
        return Ok(account);
    }
}