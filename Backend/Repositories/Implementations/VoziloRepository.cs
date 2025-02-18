using AutoMapper;
using WebTemplate.Models;

namespace WebTemplate.Repositories.Implementations;

public class VoziloRepository : IVoziloRepository
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public VoziloRepository(Context context, IKorisnikRepository korisnik, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task DodajAsync(VoziloDodavanjeDTO voziloDTO)
    {
        if (voziloDTO is MotorDodavanjeDTO motorDTO)
        {
            var motor = _mapper.Map<Motor>(motorDTO);
            _context.Add(motor);
        }
        else if (voziloDTO is AutomobilDodavanjeDTO automobilDTO)
        {
            var automobil = _mapper.Map<Automobil>(automobilDTO);
            _context.Add(automobil);
        }
        await _context.SaveChangesAsync(); 
    }

    public async Task<List<VoziloDTO>> PrikaziSveAsync()
    {
        var vozila = await _context.Vozila
            .Include(v => v.Korisnik) 
            .ToListAsync();

        List<VoziloDTO> vozilaDTO = _mapper.Map<List<VoziloDTO>>(vozila);
        return vozilaDTO;
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