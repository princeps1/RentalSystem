using Microsoft.AspNetCore.Mvc;
using RentalSystemUI.DataAccess;
using RentalSystemUI.DTOs.Korisnik;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Refit;
namespace RentalSystemUI.Controllers; 

public class KorisnikController : Controller
{
    private readonly IKorisnik _korisnikService;

    public KorisnikController(IKorisnik korisnik)
    {
        _korisnikService = korisnik;
    }

    [Microsoft.AspNetCore.Authorization.Authorize]
    [HttpGet]
    public async Task<IActionResult> KorisniciIndex()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Auth");
        }
        string token = HttpContext!.Session.GetString("JWToken")!;
        string bearerToken = $"Bearer {token}";
        var korisnici = await _korisnikService.PrikaziSve(bearerToken);
        return View(korisnici);    
    }

    [Microsoft.AspNetCore.Authorization.Authorize]
    [HttpGet]
    public IActionResult Dodaj()
    {
        return View();
    }


    [Microsoft.AspNetCore.Authorization.Authorize]
    [HttpPost]
    public async Task<IActionResult> Dodaj(string imePrezime, string jmbg, string brVozacke)
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Auth");
        }

        // Dohvati token iz sesije
        string token = HttpContext.Session.GetString("JWToken")!;
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Auth");
        }

        // Dodaj prefiks "Bearer"
        string bearerToken = $"Bearer {token}";

        try
        {
            await _korisnikService.Dodaj(imePrezime, jmbg, brVozacke, bearerToken);
            // Nakon uspešnog dodavanja, možete preusmeriti korisnika ili prikazati potvrdu
            return RedirectToAction("KorisniciIndex", "Korisnik");
        }
        catch (ApiException ex)
        {
            // Obrada greške (npr. ispis poruke greške korisniku)
            ModelState.AddModelError("", $"Greška pri dodavanju korisnika: {ex.Message}");
            return View();
        }
    }

}
