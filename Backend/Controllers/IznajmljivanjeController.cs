namespace WebTemplate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IznajmljivanjeController : ControllerBase
{
   private readonly IIznajmljivanjeService _iznajmljivanjeService;
    public IznajmljivanjeController(IIznajmljivanjeService iznajmljivanjeService)
    {
        _iznajmljivanjeService = iznajmljivanjeService;
    }


    [HttpGet("IznajmiVozilo")]
    [SwaggerResponse(StatusCodes.Status200OK, "Vozilo je uspesno iznajmljeno.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Vozilo ili korisnik nisu pronadjeni.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Vozilo je vec iznajmljeno.")]
    public async Task<IActionResult> IznajmiVoziloAsync([FromQuery,Required] int brDana,    
                                                        [FromQuery, Required] string regBroj,
                                                        [FromQuery, Required,StringLength(13)] string jmbg)
    {
        try
        {
            if (!await _iznajmljivanjeService.DaLiKorisnikPostojiAsync(jmbg))
            {
                return NotFound("Nije pronadjen korisnik");
            }
            if (!await _iznajmljivanjeService.DaLiVoziloPostojiAsync(regBroj))
            {
                return NotFound("Nije pronadjeno vozilo");
            }
            else if (await _iznajmljivanjeService.DaLiJeVoziloIznajmljeno(regBroj))
            {
                return BadRequest("Vozilo je vec iznajmljeno");
            }

            await _iznajmljivanjeService.IznajmiVoziloAsync(brDana, regBroj, jmbg);
            return Ok($"Vozilo sa registarskim brojem {regBroj} je iznajmljeno korisniku sa JMBG-om {jmbg}");

        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

}
