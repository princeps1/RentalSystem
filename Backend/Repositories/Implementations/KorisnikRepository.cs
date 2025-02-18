using AutoMapper;

namespace WebTemplate.Repositories.Implementations;

public class KorisnikRepository : IKorisnikRepository
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public KorisnikRepository(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Korisnik> DodajAsync(string imePrezime, string jmbg, string brVozacke)
    {
        var korisnik = new Korisnik
        {
            ImePrezime = imePrezime,
            JMBG = jmbg,
            BrVozacke = brVozacke
        };
        await _context.Korisnici.AddAsync(korisnik);
        await _context.SaveChangesAsync();
        return korisnik;
    }

    public async Task<Korisnik> AzurirajVozackuAsync(string jmbg, string noviBrVozacke)
    {
        var korisnik =  await _context.Korisnici.FirstOrDefaultAsync(k => k.JMBG == jmbg);

        korisnik!.BrVozacke = noviBrVozacke;
        _context.Update(korisnik);

        await _context.SaveChangesAsync();
        return korisnik;
    }

    public async Task<List<KorisnikDTO>> PrikaziSveAsync()
    {
        var korisnici = await _context.Korisnici.Include(k => k.Vozila).ToListAsync();
        List<KorisnikDTO> korisniciDTO = _mapper.Map<List<KorisnikDTO>>(korisnici);
        return korisniciDTO;
    }

    public async Task<Korisnik?> PrikaziKorisnikaAsync(string jmbg)
    {
        return await _context.Korisnici.FirstOrDefaultAsync(k => k.JMBG == jmbg);
    }

    public async Task ObrisiAsync(string jmbg)
    {

        var korisnik = await _context.Korisnici.Include(k => k.Vozila).FirstOrDefaultAsync(k => k.JMBG == jmbg);

        _context.Korisnici.Remove(korisnik!);
        await _context.SaveChangesAsync();

    }







    //HELPERI
    public async Task<bool> DaLiPostojiAsync(string? jmbg = null, string? brVozacke = null)
    {
        var korisnikPostoji = false;
        if (jmbg == null && brVozacke == null)
        {
            korisnikPostoji = await _context.Korisnici.AnyAsync();
        }
        else if (brVozacke == null)
        {
            korisnikPostoji = await _context.Korisnici.AnyAsync(k => k.JMBG == jmbg);
        }
        else
        {
            korisnikPostoji = await _context.Korisnici.AnyAsync(k => k.BrVozacke == brVozacke || k.JMBG == jmbg);
        }
        return korisnikPostoji;
    }

    public async Task<bool> KorisnikJeIznajmioVoziloAsync(string? jmbg = null)
    {
        var korisnikJeIznajmioVozilo = await _context.Korisnici.Where(k => k.JMBG == jmbg)
                                               .SelectMany(k => k.Vozila!)
                                               .AnyAsync();
        return korisnikJeIznajmioVozilo;
    }

}
