using RentalSystemUI.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace RentalSystemUI.Controllers;

public class VoziloController : Controller
{
    private readonly IAuth _authService;

    public VoziloController(IAuth authService)
    {
        _authService = authService;
    }
    
    [HttpGet]
    public async Task<IActionResult> VozilaIndex()
    {
        return View();    
    }
}
