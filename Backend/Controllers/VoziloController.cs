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
    [SwaggerResponse(StatusCodes.Status200OK, "Vozilo je uspesno dodato.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Vozilo vec postoji.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Nisi poslao nikakvo vozilo.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Problem sa tipom ili bazom.")]
    public async Task<IActionResult> Dodaj([FromBody] Vozilo vozilo)
    {
        try
        {
            if (vozilo == null)
            {
                return NoContent();
            }

            if (await _voziloRepo.DaLiPostojiAsync(vozilo.RegistarskiBroj))
            {
                return Conflict("Vozilo sa tim registarskim brojem vec postoji.");
            }

            var result = await _voziloRepo.DodajAsync(vozilo);
            return Ok($"Vozilo uspesno dodatno sa ID-em {result.ID}");
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [HttpGet("PrikaziSve")]
    [SwaggerResponse(StatusCodes.Status200OK, "Vozila su uspesno prikazana.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Nema vozila u bazi trenutno.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Problem sa bazom.")]
    public async Task<IActionResult> PrikaziSve()
    {
        try
        {
            if (!await _voziloRepo.DaLiPostojiAsync() )
            {
                return NotFound("Nema vozila u bazi podataka.");
            }
            var result =  await _voziloRepo.PrikaziSveAsync();
            return Ok(result);
        }
        catch (Exception)
        {

            return BadRequest();
        }
    }

    [SwaggerResponse(StatusCodes.Status200OK, "Vozilo je uspesno prikazano.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Nema ovog vozila u bazi trenutno.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Problem sa bazom.")]
    [HttpGet("PrikaziVozilo")]
    public async Task<IActionResult> PrikaziVozilo([FromQuery , Required ] int id)
    {
        try
        {

            var result = await _voziloRepo.PrikaziVoziloAsync(id);
            if (result == null)
            {
                return NotFound("Nema ovog vozila u bazi trenutno.");
            }
            return Ok(result);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    [HttpDelete("Obrisi")]
    [SwaggerResponse(StatusCodes.Status200OK, "Vozilo je uspesno obrisano.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Vozilo sa zadatim ID-em ne postji u bazi.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Problem sa bazom.")]
    public async Task<IActionResult> Obrisi([FromQuery, Required] int id)
    {
        try
        {
            return await _voziloRepo.ObrisiAsync(id);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

}
