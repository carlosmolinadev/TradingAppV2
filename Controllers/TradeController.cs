using Microsoft.AspNetCore.Mvc;
using TradingAppMvc.Application.Interfaces.Services;
using TradingAppMvc.Application.Models.Pages;
using TradingAppMvc.Views.Components.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TradingAppMvc.Controllers;

[ApiController]
[Route("[controller]")]
public class TradeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly IAccountService _accountService;
    private readonly IPromptService _promptService;
    public TradeController(ILogger<HomeController> logger, IAccountService accountService, IPromptService promptService)
    {
        _logger = logger;
        _accountService = accountService;
        _promptService = promptService;
    }

    [HttpGet("futures")]
    public async Task<IActionResult> Futures()
    {
        var model = new FuturesPage();
        var modelResponse = await _promptService.GetFuturesPageData(new Guid("c96666c1-2ed3-4888-b775-9f4ebe277bed"), model);
        if (!modelResponse.Success)
        {
            ViewBag.Error = modelResponse.ErrorMessage;
            return View(model);
        }
        return View(modelResponse.Result);
    }

    [HttpGet("futures/prompt-option/balance")]
    public async Task<IResult> GetFuturesPrompt(int exchange)
    {
        var promptResult = await _promptService.GetBalanceOptions(new Guid("c96666c1-2ed3-4888-b775-9f4ebe277bed"), exchange);
        if (!promptResult.Success)
        {
            return new RazorComponentResult<SelectOptionComponent>(new { Error = promptResult.ErrorMessage });
        }
        return new RazorComponentResult<SelectOptionComponent>(new { Options = promptResult.Result });
    }

    // [HttpPost("futures/trade-setting")]
    // public async Task<IResult> CreateTradeSetting(string exchange)
    // {
    //     var promptResult = await _promptService.GetAvailableBalancePrompt(new Guid("c96666c1-2ed3-4888-b775-9f4ebe277bed"), exchange);
    //     return new RazorComponentResult<PromptGenerator>(new { Prompt = promptResult });
    // }

}