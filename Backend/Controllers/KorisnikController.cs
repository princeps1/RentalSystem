
namespace WebTemplate.KorisnikController;

[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{

    private readonly IKorisnikRepository _korisnikRepo;

    public KorisnikController(IKorisnikRepository korisnik)
    {
        _korisnikRepo = korisnik;
    }

    [HttpPost("Dodaj")]
    public async Task<IActionResult> Dodaj(string imePrezime, string jmbg, int brVozacke)
    {
        return await _korisnikRepo.DodajAsync(imePrezime, jmbg, brVozacke);
    }

    [HttpPut("Azuriraj/{id}/{brVozacke}")]
    public async Task<IActionResult> Azuriraj(int id,int brVozacke)
    {
        return await _korisnikRepo.AzurirajVozackuAsync(id,brVozacke);
    }

    [HttpGet("PrikaziSve")]
    public async Task<IActionResult> PrikaziSve()
    {
        return await _korisnikRepo.PrikaziSveAsync();
    }

    [HttpDelete("Obrisi/{id}")]
    public async Task<IActionResult> Obris(int id)
    {
        return await _korisnikRepo.ObrisiAsync(id);
    }


}