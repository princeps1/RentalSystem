using RentalSystemUI.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RentalSystemUI.DTOs.Auth;

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

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, result!.User!.UserName!));
            //identity.AddClaim(new Claim(ClaimTypes.Role, result!.User!.Role!));
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
