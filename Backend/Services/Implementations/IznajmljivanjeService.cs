


using WebTemplate.Data;

namespace WebTemplate.Services.Implementations;

public class IznajmljivanjeService : IIznajmljivanjeService
{
    private readonly IVoziloRepository _voziloRepo;
    private readonly IKorisnikRepository _korisnikRepo;
    private readonly IUnitOfWork _unitOfWork;


    public IznajmljivanjeService(IVoziloRepository voziloRepo, IKorisnikRepository korisnikRepo, IUnitOfWork unitOfWork)
    {
        _voziloRepo = voziloRepo;
        _korisnikRepo = korisnikRepo;
        _unitOfWork = unitOfWork;
    }
    public async Task<ActionResult> IznajmiVoziloAsync(int brDana, int idVozila, int idKorisnika)
    {
        try
        {
            var korisnik = await _korisnikRepo.PrikaziKorisnikaAsync(idKorisnika);
            if (korisnik == null)
            {
                return new NotFoundObjectResult("Nije pronadjen korisnik");
            }

            // Učitaj vozilo i proveri dostupnost
            var vozilo = await _voziloRepo.PrikaziVoziloAsync(idVozila);
            if (vozilo == null)
            {
                return new NotFoundObjectResult("Nije pronadjeno vozilo");
            }
            if (vozilo.Korisnik != null)
            {
                return new BadRequestObjectResult("Vozilo je vec iznajmljeno");
            }

            vozilo.Korisnik = korisnik;
            vozilo.Iznajmljen = true;
            vozilo.BrDanaIznajmljivanja = brDana;
            korisnik.Vozila!.Add(vozilo);

            await _unitOfWork.CommitAsync();

            return new OkObjectResult($"Vozilo sa ID {vozilo.ID} je iznajmljeno korisniku {korisnik.ImePrezime}");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}
