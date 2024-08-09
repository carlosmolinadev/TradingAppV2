using System;
using Microsoft.AspNetCore.Mvc;
using TradingAppMvc.Models.Views;
using TradingAppMvc.Models.Requests;
using System.Text.Json;
using System.Diagnostics;
using TradingAppMvc.Services;
using Microsoft.Net.Http.Headers;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TradingAppMvc.Models.Mappings;

namespace TradingAppMvc.Controllers;

[Route("[controller]")]
public class TradeController : Controller
{

   private readonly IPromptService _promptService;
   public TradeController(IPromptService promptService)
   {
      _promptService = promptService;
   }

   [HttpGet("")]
   public async Task<IActionResult> Index()
   {
      var model = new CreateTradeModel();
      return View();
   }

   [HttpGet("create-trade")]
   public async Task<IActionResult> CreateTrade([FromHeader] Guid userId)
   {
      var model = new CreateTradeModel();
      var optionsRequest = await _promptService.GetCreateTradeOptionLists(userId);
      if (!optionsRequest.Success)
      {
         ViewBag.ErrorMessage = optionsRequest.ErrorMessage;
         return View(model);
      }
      ViewBag.Exchanges = optionsRequest.Data.Exchanges;
      ViewBag.Symbols = optionsRequest.Data.Symbols;
      ViewBag.OrderTypes = optionsRequest.Data.OrderTypes;
      model.Request.ExchangeId = 2;
      var tradingSettingsRequest = await _promptService.GetTradingSettings(userId);
      model.Request.TradingSettingId = Guid.NewGuid();
      model.Request.TradingSettingId.ToString();

      return View(model);
   }

   [HttpPost("create-trade")]
   public async Task<IActionResult> CreateFuturesTrade(CreateTradeRequest data)
   {
      // if (Request.Cookies["userId"] == null)
      // {
      //    return BadRequest();
      // }
      var userId = new Guid(Request.Cookies["userId"]!);
      if (!ModelState.IsValid)
      {
         var errors = ModelState.Where(e => e.Value!.Errors.Any());
         var model = errors.Select(e => new ValidationError(e.Key, e.Value!.Errors.Select(e => e.ErrorMessage)));
         Response.StatusCode = 400;
         return PartialView("_ValidationError", model);
      }
      var optionsRequest = await _promptService.GetCreateTradeOptionLists(userId);
      // data.CreateTradeOptionList = optionsRequest.Data;
      return PartialView("_CreateTrade", data);
   }


   // [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
   [Authorize]
   [HttpGet("futures/balance")]
   public async Task<IResult> GetFuturesBalance(int exchangeId)
   {
      return Results.Content("<p>200.00</p>", "text/html");
   }

   [HttpGet("test-request")]
   public async Task<IActionResult> GetTradeSetting()
   {
      return PartialView("_TradingSetting", new TradeSettingRequest());
   }
   // [HttpPost("futures/trade-setting")]
   // public async Task<IResult> CreateTradeSetting(string exchange)
   // {
   //     var promptResult = await _promptService.GetAvailableBalancePrompt(new Guid("c96666c1-2ed3-4888-b775-9f4ebe277bed"), exchange);
   //     return new RazorComponentResult<PromptGenerator>(new { Prompt = promptResult });
   // }

}
