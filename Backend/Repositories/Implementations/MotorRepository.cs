using WebTemplate.Data;
using WebTemplate.Repositories.Interfaces;

namespace WebTemplate.Repositories.Implementations;
public class MotorRepository : IMotor
{
    private readonly Context _context;

    public MotorRepository(Context context)
    {
        _context = context;
    }
    public async Task<ActionResult> DodajAsync([FromBody] Motor motor)
    {
        try
        {
            await _context.Motori.AddAsync(motor);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Motor je uspešno dodat u bazu.");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }

    public async Task<ActionResult> PrikaziSveMarkeAsync()
    {
        try
        {
            var marka = await _context.Motori.Select(s => s.Marka).ToListAsync();
            return new OkObjectResult(marka);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}