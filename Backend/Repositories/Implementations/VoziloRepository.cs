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

    public async Task<Vozilo?> PrikaziVoziloAsync(string regBroj)
    {
        return await _context.Vozila.FirstOrDefaultAsync(k => k.RegistarskiBroj == regBroj);
    }

    public async Task ObrisiAsync(string regBroj)
    {
        var vozilo = await _context.Vozila.FirstOrDefaultAsync(k => k.RegistarskiBroj == regBroj);
        _context.Vozila.Remove(vozilo!);
        await _context.SaveChangesAsync();
    }


    //HELPERI
    public async Task<bool> DaLiPostojiAsync(string? regBroj = null)
    {
        if(regBroj != null)
        {
            return await _context.Vozila.AnyAsync(c => c.RegistarskiBroj == regBroj);
        }
        return await _context.Vozila.AnyAsync();
    }

    public async Task<bool> DaLiJeIznajmljeno(string regBr)
    {
        return await _context.Vozila.AnyAsync(c => c.RegistarskiBroj == regBr && 
                                                   c.Iznajmljen && 
                                                   c.BrDanaIznajmljivanja != 0 &&   
                                                   c.Korisnik != null);
    }


}