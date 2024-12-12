using WebTemplate.Data;
using WebTemplate.Repositories.Interfaces;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class VoziloController : ControllerBase
{
    private readonly IVozilo voziloRepo;
    public VoziloController(IVozilo vozilo)
    {
        voziloRepo = vozilo;
    }



    [HttpPost("Dodaj")]
    public async Task<IActionResult> Dodaj([FromBody] Vozilo vozilo)
    {
        return await voziloRepo.DodajAsync(vozilo);
    }

    //U okviru ove funkcije se dodaje KORISNIK
    [HttpPut("Iznajmi/{imePrezime}/{jmbg}/{brVozacke}/{brIznajmljivanja}/{idVozila}")]
    public async Task<IActionResult> Iznajmi(string imePrezime, string jmbg, int brVozacke, int brIznajmljivanja, int idVozila)
    {
        return await voziloRepo.IznajmiAsync(imePrezime, jmbg, brVozacke, brIznajmljivanja, idVozila);
    }
    [HttpGet("PrikaziSve")]
    public async Task<IActionResult> PrikaziSve()
    {
        return await voziloRepo.PrikaziSveAsync();
    }


    [HttpDelete("Obrisi/{id}")]
    public async Task<IActionResult> Obrisi(int id)
    {
        return await voziloRepo.ObrisiAsync(id);
    }


















}
