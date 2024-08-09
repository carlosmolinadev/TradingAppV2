using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Models.Mappings;
using TradingAppMvc.Models.Requests;
using TradingAppMvc.Repositories;
using TradingAppMvc.Services;

namespace TradingAppMvc.Controllers
{
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly ISettingsService _settingsService;
        public SettingsController(ILogger<SettingsController> logger, ISettingsService settingsService)
        {
            _logger = logger;
            _settingsService = settingsService;
        }

        [HttpPost("apikey")]
        public async Task<IActionResult> AddApiKey([FromForm] ApiKeyRequest request)
        {
            if (Request.Cookies["userId"] == null)
            {
                return BadRequest();
            }
            var userId = new Guid(Request.Cookies["userId"]!);
            var response = await _settingsService.SetApiKey(userId, request);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }
            return Ok(response.Data);
        }
    }
}