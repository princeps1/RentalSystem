using WebTemplate.Models;

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
    public async Task<IActionResult> Dodaj([FromQuery , Required]string imePrezime, 
                                           [FromQuery, Required , StringLength(13)] string jmbg, 
                                           [FromQuery, Required] int brVozacke)
    {
        try
        {
            if (await _korisnikRepo.DaLiPostoji(jmbg, brVozacke))
            {
                return Conflict("Korisnik sa tim JMBG-om i brojem vozacke dozvole vec postoji.");
            }
            var korisnik = await _korisnikRepo.DodajAsync(imePrezime, jmbg, brVozacke);
            return Ok($"Dodat je korisnik {imePrezime}");

        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut("Azuriraj")]
    public async Task<IActionResult> AzurirajVozacku([FromQuery, Required , StringLength(13)] string jmbg,
                                                     [FromQuery, Required] int noviBrVozacke)
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostoji(jmbg))
            {
                return NoContent();
            }
            var korisnik = await _korisnikRepo.AzurirajVozackuAsync(jmbg, noviBrVozacke);
            return Ok("Korisnik je azuriran!");
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("PrikaziSve")]
    public async Task<IActionResult> PrikaziSve()
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostoji())
            {
                return NoContent();
            }
            var korisnici = await _korisnikRepo.PrikaziSveAsync();
            return Ok(korisnici);
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [HttpGet("PrikaziKorisnika")]
    public async Task<IActionResult> PrikaziKorisnika([FromQuery, Required, StringLength(13)] string jmbg)
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostoji(jmbg))
            {
                return NoContent();
            }
            var korisnik = await _korisnikRepo.PrikaziKorisnikaAsync(jmbg);
            return Ok(korisnik);
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [HttpDelete("Obrisi")]
    public async Task<IActionResult> Obris([FromQuery, Required, StringLength(13)] string jmbg)
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostoji(jmbg))
            {
                return NoContent();
            }
            if (await _korisnikRepo.KorisnikJeIznajmioVozilo(jmbg))
            {
                return BadRequest("Korisnik je iznajmio vozilo i ne moze biti obrisan!");
            }
            await _korisnikRepo.ObrisiAsync(jmbg);
            return Ok("Korisnik je obrisan!");
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


}