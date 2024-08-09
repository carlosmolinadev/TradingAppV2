using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradingAppMvc.Models.Requests;
using TradingAppMvc.Models.Responses;
using TradingAppMvc.Services;

namespace TradingAppMvc.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITradeService _tradeService;

        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] LoginRequest request)
        {
            var response = new ApplicationResponse<Microsoft.AspNetCore.Identity.SignInResult>(default);
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                response.SetError("User cannot be null");
                return BadRequest(response);
            }
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                response.SetError("User cannot be null");
                return BadRequest(response);
            }
            var signIn = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!signIn.Succeeded)
            {
                response.SetError("Invalid username or password");
                return BadRequest(response);
            }

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role, "Admin"),
                // new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            Response.Cookies.Append("userId", user.Id);
            return Redirect("~/");
            // return RedirectToAction("Index", "Home");
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromForm] RegisterUserRequest request)
        {
            var response = new ApplicationResponse<Microsoft.AspNetCore.Identity.IdentityResult>(default);
            if (request == null)
            {
                response.SetError("User cannot be null");
                return Results.BadRequest(response);
            }
            if (request.Username == null)
            {
                response.SetError("User cannot be null");
                return Results.BadRequest(response);
            }
            var applicationUser = await _userManager.FindByNameAsync(request.Username);
            if (applicationUser != null)
            {
                response.SetError("User cannot be null");
                return Results.BadRequest(response);
            }
            var identityUser = new IdentityUser()
            {
                UserName = request.Username
            };
            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (!result.Succeeded)
            {
                return Results.Ok(identityUser);
            }
            response.SetResult(result);
            return Results.Ok(response);
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}