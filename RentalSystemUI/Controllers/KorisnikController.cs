using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystemUI.DataAccess;
using RentalSystemUI.DTOs.Korisnik;
namespace RentalSystemUI.Controllers;


[Authorize]
[Route("[controller]")]
public class KorisnikController : Controller
{
    private readonly IKorisnik _korisnikService;

    public KorisnikController(IKorisnik korisnik)
    {
        _korisnikService = korisnik;
    }

    // GET /Korisnik (lista korisnika)
    [HttpGet("")]
    public async Task<IActionResult> KorisniciIndex()
    {
        if (!User.Identity!.IsAuthenticated)
            return RedirectToAction("Login", "Auth");

        string token = HttpContext.Session.GetString("JWToken")!;
        string bearerToken = $"Bearer {token}";
        var korisnici = await _korisnikService.PrikaziSve(bearerToken);
        return View(korisnici); // View: KorisniciIndex.cshtml
    }

    // GET /Korisnik/PrikaziKorisnika/{jmbg}
    [HttpGet("PrikaziKorisnika/{jmbg}")]
    public async Task<IActionResult> PrikaziKorisnika(string jmbg)
    {
        if (!User.Identity!.IsAuthenticated)
            return RedirectToAction("Login", "Auth");

        string token = HttpContext.Session.GetString("JWToken")!;
        string bearerToken = $"Bearer {token}";
        var korisnik = await _korisnikService.PrikaziKorisnika(jmbg, bearerToken);

        if (korisnik == null)
            return NotFound();

        // View: PrikaziKorisnika.cshtml
        return View(korisnik);
    }





    // GET /Korisnik/Dodaj
    [HttpGet("Dodaj")]
    public IActionResult Dodaj()
    {
        return View();
    }

    // POST /Korisnik/Dodaj
    [HttpPost("Dodaj")]
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
            return RedirectToAction("KorisniciIndex", "Korisnik");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Greška pri dodavanju korisnika: {ex.Message}");
            return View();
        }
    }



    // GET /Korisnik/AzurirajVozacku/{jmbg}
    [HttpGet("AzurirajVozacku/{jmbg}")]
    public async Task<IActionResult> AzurirajVozacku(string jmbg)
    {
        if (!User.Identity!.IsAuthenticated)
            return RedirectToAction("Login", "Auth");

        string token = HttpContext.Session.GetString("JWToken")!;
        string bearerToken = $"Bearer {token}";

        // Preuzmi trenutne podatke korisnika (DTO)
        var korisnik = await _korisnikService.PrikaziKorisnika(jmbg, bearerToken);
        if (korisnik == null)
            return NotFound();

        // Prosleđujemo DTO direktno view-u
        return View(korisnik);  // View: /Views/Korisnik/AzurirajVozacku.cshtml
    }



    [HttpPost("AzurirajVozacku/{jmbg}")]
    public async Task<IActionResult> AzurirajVozacku(string jmbg, RentalSystemUI.DTOs.Korisnik.KorisnikDTO model)
    {
        if (!User.Identity!.IsAuthenticated)
            return RedirectToAction("Login", "Auth");

        string token = HttpContext.Session.GetString("JWToken")!;
        string bearerToken = $"Bearer {token}";

        try
        {
            // Pozovi API za ažuriranje vozačke, prosleđujući novi broj vozačke kao query parametar
            var responseMessage = await _korisnikService.AzurirajVozacku(jmbg, model.BrVozacke, bearerToken);

            // Nakon uspešnog ažuriranja, preusmeri na detalje korisnika
            return RedirectToAction("PrikaziKorisnika", new { jmbg = jmbg });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Greška pri ažuriranju: {ex.Message}");
            return View(model);
        }
    }

    [HttpPost("ObrisiKorisnika/{jmbg}")]
    public async Task<IActionResult> ObrisiKorisnika(string jmbg)
    {
        if (!User.Identity!.IsAuthenticated)
            return RedirectToAction("Login", "Auth");

        string? token = HttpContext.Session.GetString("JWToken");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login", "Auth");

        string bearerToken = $"Bearer {token}";

        try
        {
            await _korisnikService.Obris(jmbg, bearerToken);
            return RedirectToAction("KorisniciIndex", "Korisnik");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Greška pri brisanju: {ex.Message}");
            return RedirectToAction("PrikaziKorisnika", new { jmbg = jmbg });
        }
    }

}
