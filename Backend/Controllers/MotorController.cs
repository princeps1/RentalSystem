using WebTemplate.Repositories.Interfaces;
namespace WebTemplate.MotorController;

[ApiController]
[Route("[controller]")]
public class MotorController : ControllerBase
{
    private readonly IMotor motorRepo;

    public MotorController(IMotor motor)
    {
        motorRepo = motor;
    }

    [HttpPost("Dodaj")]
    public async Task<IActionResult> Dodaj([FromBody] Motor motor)
    {
        return await motorRepo.DodajAsync(motor);
    }

    [HttpGet("PrikaziSveMarke")]
    public async Task<IActionResult> PrikaziSveMarke()
    {
        return await motorRepo.PrikaziSveMarkeAsync();
    }

}