namespace RentalSystem.Controllers;

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
    public async Task<IActionResult> Dodaj([FromBody] VoziloDodavanjeDTO voziloDTO)
    {
        try
        {
            if (voziloDTO == null)
            {
                return NoContent();
            }

            if (await _voziloRepo.DaLiPostojiAsync(voziloDTO.RegistarskiBroj))
            {
                return Conflict("Vozilo sa tim registarskim brojem vec postoji.");
            }

            await _voziloRepo.DodajAsync(voziloDTO);
            return Ok("Vozilo uspesno dodatno");
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("PrikaziSve")]
    [SwaggerResponse(StatusCodes.Status200OK, "Vozila su uspesno prikazana.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Nema vozila u bazi trenutno.")]
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
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [SwaggerResponse(StatusCodes.Status200OK, "Vozilo je uspesno prikazano.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Nema ovog vozila u bazi trenutno.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Problem sa bazom.")]
    [HttpGet("PrikaziVozilo")]
    public async Task<IActionResult> PrikaziVozilo([FromQuery , Required ] string regBroj)
    {
        try
        {
            if (!await _voziloRepo.DaLiPostojiAsync(regBroj))
            {
                return NotFound("Nema ovog vozila u bazi trenutno.");
            }

            var result = await _voziloRepo.PrikaziVoziloAsync(regBroj);
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
    public async Task<IActionResult> Obrisi([FromQuery, Required] string regBroj)
    {
        try
        {
            if(!await _voziloRepo.DaLiPostojiAsync(regBroj))
            {
                return NotFound("Nije pronadjeno vozilo u bazi");
            }
            if(await _voziloRepo.DaLiJeIznajmljeno(regBroj))
            {
                return BadRequest("Vozilo je iznajmljeno i ne moze se obrisati");
            }
            await _voziloRepo.ObrisiAsync(regBroj);
            return Ok("Vozilo je obrisano");
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

}
