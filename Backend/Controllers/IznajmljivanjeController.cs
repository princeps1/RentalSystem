
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
    public async Task<IActionResult> IznajmiVoziloAsync(int brDana,int idVozila,int idKorisnika)
    {
        return await _iznajmljivanjeService.IznajmiVoziloAsync(brDana, idVozila, idKorisnika);
    }

}
