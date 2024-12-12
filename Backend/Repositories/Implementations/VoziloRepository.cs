using Microsoft.AspNetCore.Http.HttpResults;
using WebTemplate.Data;
using WebTemplate.Repositories.Interfaces;

namespace WebTemplate.Repositories.Implementations;

public class VoziloRepository : IVozilo
{
    private readonly Context _context;
    private readonly IKorisnik korisnikRepo;

    public VoziloRepository(Context context, IKorisnik korisnik)
    {
        _context = context;
        korisnikRepo = korisnik;
    }


    public async Task<ActionResult> DodajAsync(Vozilo vozilo)
    {
        try
        {
            if (vozilo is Motor motor)
            {
                _context.Add(motor);
            }
            else if (vozilo is Automobil automobil)
            {
                _context.Add(automobil);
            }
            else
            {
                return new BadRequestObjectResult("Nepoznat tip vozila.");
            }

            await _context.SaveChangesAsync();
            return new OkObjectResult("Vozilo uspešno dodato.");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }

    public async Task<ActionResult> IznajmiAsync(string imePrezime, string jmbg, int brVozacke, int brIznajmljivanja, int idVozila){
        try
        {
            var vozilo = _context.Vozila.Find(idVozila);
            if (vozilo == null)
                return new NotFoundObjectResult("Vozilo nije pronadjeno");
            if (vozilo.BrDanaIznajmljivanja > 0 && vozilo.Iznajmljen)
            {
                return new BadRequestObjectResult("Vozilo je vec iznajmljeno");
            }
            await korisnikRepo.DodajAsync(imePrezime, jmbg, brVozacke, brIznajmljivanja, vozilo);
            vozilo!.BrDanaIznajmljivanja = brIznajmljivanja;
            vozilo.Iznajmljen = true;
            await _context.SaveChangesAsync();

            return new OkObjectResult($"Vozilo {vozilo.Marka} je uspesno iznajmljeno");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
     }
    
    
    public async Task<ActionResult> PrikaziSveAsync()
    {
        var vozila = await _context.Vozila
            .Select(v => new
            {
                v.ID,
                v.Marka,
                v.Model,
                BrSedista = v is Automobil ? ((Automobil)v).BrSedista : (int?)null
            })
            .ToListAsync(); // Asinhrono preuzimanje izabranih svojstava

        if (vozila == null || vozila.Count == 0)
        {
            return new NotFoundObjectResult("Nema vozila u bazi podataka.");
        }

        return new OkObjectResult(vozila); // Vraća selektovana svojstva sa statusom 200 OK
    }

    public async Task<ActionResult> ObrisiAsync(int id)
    {
        try
        {
            var vozilo = await _context.Vozila.FindAsync(id);
            if (vozilo == null)
            {
                return new NotFoundObjectResult("Vozilo sa zadatimID-em nije pronadjeno");
            }
            _context.Vozila.Remove(vozilo);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Uspesno obrisano vozilo");
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