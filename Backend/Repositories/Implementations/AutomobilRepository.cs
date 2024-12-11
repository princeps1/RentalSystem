using WebTemplate.Data;
using WebTemplate.Repositories.Interfaces;

namespace WebTemplate.Repositories.Implementations;

public class AutomobilRepository : IAutomobil
{
    private readonly Context _context;

    public AutomobilRepository(Context context)
    {
        _context = context;
    }

    public async Task<ActionResult> DodajAsync([FromBody] Automobil automobil)
    {
        try
        {
            await _context.Automobili.AddAsync(automobil);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Automobil je uspešno dodat u bazu.");
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
            var marke = await _context.Automobili.Select(s => s.Marka).ToListAsync();
            return new OkObjectResult(marke);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
