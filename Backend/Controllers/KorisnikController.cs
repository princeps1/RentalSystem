using WebTemplate.Repositories.Interfaces;

namespace WebTemplate.KorisnikController;

[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{

    private readonly IKorisnik korisnikRepo;

    public KorisnikController(IKorisnik korisnik)
    {
        korisnikRepo = korisnik;
    }

    [HttpPut("Azuriraj/{id}/{brVozacke}")]
    public async Task<IActionResult> Azuriraj(int id,int brVozacke)
    {
        return await korisnikRepo.AzurirajAsync(id,brVozacke);
    }

    [HttpGet("PrikaziSve")]
    public async Task<IActionResult> PrikaziSve()
    {
        return await korisnikRepo.PrikaziSveAsync();
    }

    [HttpDelete("Obrisi/{id}")]
    public async Task<IActionResult> Obris(int id)
    {
        return await korisnikRepo.ObrisiAsync(id);
    }


}