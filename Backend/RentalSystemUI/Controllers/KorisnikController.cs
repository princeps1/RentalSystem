using Microsoft.AspNetCore.Mvc;
using RentalSystemUI.DataAccess;
using RentalSystemUI.DTOs.Korisnik;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
namespace RentalSystemUI.Controllers; 

public class KorisnikController : Controller
{
    private readonly IKorisnik _korisnikService;

    public KorisnikController(IKorisnik korisnik)
    {
        _korisnikService = korisnik;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> KorisniciIndex()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Auth");
        }
        string token = HttpContext!.Session.GetString("JWToken");
        string bearerToken = $"Bearer {token}";
        var korisnici = await _korisnikService.PrikaziSve(bearerToken);
        return View(korisnici);    
    }
}
