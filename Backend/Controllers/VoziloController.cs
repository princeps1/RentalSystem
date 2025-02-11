using WebTemplate.Data;
using WebTemplate.Repositories.Interfaces;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class VoziloController : ControllerBase
{
    private readonly IVoziloRepository _voziloRepo;
    public VoziloController(IVoziloRepository vozilo)
    {
        _voziloRepo = vozilo;
    }

    [HttpPost("Dodaj")]
    public async Task<IActionResult> Dodaj([FromBody] Vozilo vozilo)
    {
        return await _voziloRepo.DodajAsync(vozilo);
    }

    [HttpGet("PrikaziSve")]
    public async Task<IActionResult> PrikaziSve()
    {
        return await _voziloRepo.PrikaziSveAsync();
    }

    [HttpDelete("Obrisi/{id}")]
    public async Task<IActionResult> Obrisi(int id)
    {
        return await _voziloRepo.ObrisiAsync(id);
    }


















}
