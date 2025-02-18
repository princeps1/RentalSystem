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
    [SwaggerResponse(StatusCodes.Status200OK, "Korisnik je uspesno dodato.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Korisnik vec postoji.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Greska.")]
    public async Task<IActionResult> Dodaj([FromQuery , Required]string imePrezime, 
                                           [FromQuery, Required , StringLength(13)] string jmbg, 
                                           [FromQuery, Required] string brVozacke)
    {
        try
        {
            if (await _korisnikRepo.DaLiPostojiAsync(jmbg, brVozacke))
            {
                return Conflict("Korisnik sa tim JMBG-om i brojem vozacke dozvole vec postoji.");
            }
            var korisnik = await _korisnikRepo.DodajAsync(imePrezime, jmbg, brVozacke);
            return Ok($"Dodat je korisnik {imePrezime}");

        }
        catch (Exception)
        {
            return BadRequest("Greska");
        }
    }


    [HttpPut("Azuriraj")]
    [SwaggerResponse(StatusCodes.Status200OK, "Korisnik je uspeno azuriran.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Nema korisnika u bazi trenutno.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Greska.")]
    public async Task<IActionResult> AzurirajVozacku([FromQuery, Required , StringLength(13)] string jmbg,
                                                     [FromQuery, Required] string noviBrVozacke)
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostojiAsync(jmbg))
            {
                return NotFound("Nije pronadjen korisnik");
            }
            var korisnik = await _korisnikRepo.AzurirajVozackuAsync(jmbg, noviBrVozacke);
            return Ok($"Korisnik je azuriran!");
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    [HttpGet("PrikaziSve")]
    [SwaggerResponse(StatusCodes.Status200OK, "Korisnici su uspesno prikazana.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Nema korisnika u bazi trenutno.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Greska.")]
    public async Task<IActionResult> PrikaziSve()
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostojiAsync())
            {
                return NotFound("Nema korisnika u bazi");
            }
            var korisnici = await _korisnikRepo.PrikaziSveAsync();
            return Ok(korisnici);
        }
        catch (Exception)
        {
            return BadRequest("Greska");
        }
    }


    [HttpGet("PrikaziKorisnika")]
    [SwaggerResponse(StatusCodes.Status200OK, "Korisnik je uspesno prikazan.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Nema ovog korisnika u bazi trenutno.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Greska.")]
    public async Task<IActionResult> PrikaziKorisnika([FromQuery, Required, StringLength(13)] string jmbg)
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostojiAsync(jmbg))
            {
                return NotFound("Nema ovog korisnika u bazi trenutno.");
            }
            var korisnik = await _korisnikRepo.PrikaziKorisnikaAsync(jmbg);
            return Ok(korisnik);
        }
        catch (Exception)
        {
            return BadRequest("Greska");
        }
    }


    [HttpDelete("Obrisi")]
    [SwaggerResponse(StatusCodes.Status200OK, "Korisnik je uspesno obrisan.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Nema ovog korisnika u bazi trenutno.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Korisnik je iznajmio vozilo i ne moze biti obrisan.")]
    public async Task<IActionResult> Obris([FromQuery, Required, StringLength(13)] string jmbg)
    {
        try
        {
            if (!await _korisnikRepo.DaLiPostojiAsync(jmbg))
            {
                return NotFound("Nije pronadjen korisnik");
            }
            if (await _korisnikRepo.KorisnikJeIznajmioVoziloAsync(jmbg))
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