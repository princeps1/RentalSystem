

namespace WebTemplate.Repositories.Implementations;

public class KorisnikRepository : IKorisnikRepository
{
    private readonly Context _context;

    public KorisnikRepository(Context context)
    {
        _context = context;
    }


    public async Task<ActionResult> DodajAsync(string imePrezime, string jmbg, int brVozacke)
    {
        try
        {
            var korisnikPostoji = await _context.Korisnici.AnyAsync(k => k.BrVozacke == brVozacke || k.JMBG == jmbg);
            if (korisnikPostoji)
            {
                return new BadRequestObjectResult("Korisnik vec postoji");
            }


            var korisnik = new Korisnik
            {
                ImePrezime = imePrezime,
                JMBG = jmbg,
                BrVozacke = brVozacke
            };
            await _context.Korisnici.AddAsync(korisnik);
            await _context.SaveChangesAsync();
            return new OkObjectResult($"Korisnik {korisnik.ImePrezime} je dodat\n");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
    
    public async Task<ActionResult> AzurirajVozackuAsync(int idKorisnika, int brVozacke){
        try
        {
            var korisnik = _context.Korisnici.Find(idKorisnika);
            if (korisnik == null){
                return new NotFoundObjectResult("Nije pronadjen korisnik sa zadatim ID-em");
            }
            korisnik.BrVozacke = brVozacke;
            _context.Update(korisnik);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Korisnik je uspesno azuriran");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }

    public async Task<ActionResult> PrikaziSveAsync(){
        try
        {
            var korisnik = await _context.Korisnici
            .Select(k => new
            {
                k.ImePrezime,
                k.JMBG,
                k.BrVozacke,
                Vozila = k.Vozila!.Select(v => new{
                    v.ID,
                    v.Marka,
                    v.RegistarskiBroj,
                    v!.BrDanaIznajmljivanja
                }).ToList()
            }).ToListAsync();
            return new OkObjectResult(korisnik);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }

    }

    public async Task<Korisnik?> PrikaziKorisnikaAsync(int id)
    {
        try
        {
            return await _context.Korisnici.FindAsync(id);
        }
        catch
        {
            return null; 
        }
    }



    //Proveriti da li logika za brisanje iznajmljenih vozila radi - NIJE DOBRA
    public async Task<ActionResult> ObrisiAsync(int id){
        try
        {
            var korisnik = await _context.Korisnici.Include(k => k.Vozila).FirstOrDefaultAsync(k => k.ID == id);

            if (korisnik == null)
            {
                return new NotFoundObjectResult("Korisnik sa zadatim ID-em nije pronađen.");
            }

            if (korisnik.Vozila != null && korisnik.Vozila.Count > 0)
            {
                foreach (var vozilo in korisnik.Vozila)
                {
                    vozilo.Iznajmljen = false;
                    vozilo.BrDanaIznajmljivanja = 0;
                }
            }

            _context.Korisnici.Remove(korisnik);
            await _context.SaveChangesAsync();

            return new OkObjectResult($"Korisnik sa ID-em {id} je uspešno obrisan.");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult($"Došlo je do greške prilikom brisanja: {e.Message}");
        }
    }
    public void Dispose(){
        throw new NotImplementedException();
    }
}
