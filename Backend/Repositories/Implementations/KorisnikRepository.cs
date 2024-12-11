using WebTemplate.Data;
using WebTemplate.Repositories.Interfaces;

namespace WebTemplate.Repositories.Implementations;

public class KorisnikRepository : IKorisnik
{
    private readonly Context _context;

    public KorisnikRepository(Context context)
    {
        _context = context;
    }

    public async Task<ActionResult> ObrisiAsync(int id)
    {
        try
        {
            // Pronalazak entiteta Korisnik po id-u
            var korisnik = await _context.Korisnici.FindAsync(id);

            if (korisnik == null)
            {
                return new NotFoundObjectResult("Korisnik sa zadatim ID-em nije pronađen.");
            }

            // Brisanje entiteta Korisnik
            _context.Korisnici.Remove(korisnik);
            await _context.SaveChangesAsync();

            return new OkObjectResult($"Korisnik sa ID-em {id} je uspešno obrisan.");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult($"Došlo je do greške prilikom brisanja: {e.Message}");
        }
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
