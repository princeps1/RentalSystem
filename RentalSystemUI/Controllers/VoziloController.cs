using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystemUI.DataAccess;
using RentalSystemUI.DTOs.Vozilo;

namespace RentalSystemUI.Controllers;

[Authorize]
[Route("[controller]")]
public class VoziloController : Controller
{
    private readonly IVozilo _voziloService; 
    public VoziloController(IVozilo voziloService)
    {
        _voziloService = voziloService;
    }

    [HttpGet]
    public async Task<IActionResult> VozilaIndex()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Auth");
        }
        string token = HttpContext.Session.GetString("JWToken")!;
        string bearerToken = $"Bearer {token}";
        var vozila = await _voziloService.PrikaziSve(bearerToken);
        return View(vozila);

    }
}
