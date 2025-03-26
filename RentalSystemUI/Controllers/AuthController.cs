using RentalSystemUI.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RentalSystemUI.DTOs.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace RentalSystemUI.Controllers;

public class AuthController : Controller
{
    private readonly IAuth _authService;

    public AuthController(IAuth authService)
    {
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        //if (User.Identity!.IsAuthenticated)
        //    return RedirectToAction("Index", "Home");
        LoginRequestDTO obj = new();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequestDTO obj)
    {
        var result = await _authService.Login(obj);
        if (result != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(result.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, result!.User!.UserName!));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type=="role")!.Value));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            HttpContext.Session.SetString("JWToken", result!.Token!);//ako je session toket postavljen znaci da je korisnik ulogovan
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View();
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegistrationRequestDTO obj)
    {
        var result = await _authService.Register(obj);
        if(result != null)
        {
            return RedirectToAction("Login");
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        HttpContext.Session.SetString("JWToken", "");
        return RedirectToAction("Index","Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
