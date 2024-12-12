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


    [HttpDelete("Obrisi/{id}")]
    public async Task<ActionResult> ObrisiAsync(int id)
    {
        return await korisnikRepo.ObrisiAsync(id);
    }


}