using WebTemplate.Repositories.Interfaces;


namespace WebTemplate.AutomobilController;

[ApiController]
[Route("[controller]")]
public class AutomobilController : ControllerBase
{

    private readonly IAutomobil automobilRepo;
    public AutomobilController(IAutomobil automobil)
    {
        automobilRepo = automobil;
    }


    [HttpPost("Dodaj")]
    public async Task<IActionResult> Dodaj([FromBody] Automobil automobil)
    {
        return await automobilRepo.DodajAsync(automobil);
    }

    [HttpGet("PrikaziSveMarke")]
    public async Task<IActionResult> PrikaziSveMarke()
    {
        return await automobilRepo.PrikaziSveMarkeAsync();
    }


}