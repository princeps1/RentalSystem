

namespace WebTemplate.Repositories.Implementations;

public class VoziloRepository : IVoziloRepository
{
    private readonly Context _context;

    public VoziloRepository(Context context, IKorisnikRepository korisnik)
    {
        _context = context;
    }


    public async Task<Vozilo> DodajAsync(Vozilo vozilo)
    {
        if (vozilo is Motor motor)
        {
            _context.Add(motor);
        }
        else if (vozilo is Automobil automobil)
        {
            _context.Add(automobil);
        }

        await _context.SaveChangesAsync();
        return vozilo;
    }
    
    public async Task<List<Vozilo>> PrikaziSveAsync()//Ispravicemo ovo sa DTO
    {
        var vozila = await _context.Vozila.ToListAsync();
            //.Select(v => new
            //{
            //    v.ID,
            //    v.Marka,
            //    v.Model,
            //    BrSedista = v is Automobil ? ((Automobil)v).BrSedista : (int?)null
            //})
            //.ToListAsync(); // Asinhrono preuzimanje izabranih svojstava

        return vozila; 
    }

    public async Task<Vozilo> PrikaziVoziloAsync(int id)
    {
        return await _context.Vozila.FindAsync(id);
    }

    public async Task<IActionResult> ObrisiAsync(int id)
    {
        var vozilo = await _context.Vozila.FindAsync(id);
        if (vozilo == null)
        {
            return new NotFoundObjectResult("Vozilo sa zadatim ID-em nije pronadjeno");
        }
        _context.Vozila.Remove(vozilo);
        await _context.SaveChangesAsync();
        return new OkObjectResult("Uspesno obrisano vozilo");
    }

    public async Task<bool> DaLiPostojiAsync(string? regBroj = null)
    {
        if(regBroj != null)
        {
            return await _context.Vozila.AnyAsync(c => c.RegistarskiBroj == regBroj);
        }
        return await _context.Vozila.AnyAsync();
    }

   



}