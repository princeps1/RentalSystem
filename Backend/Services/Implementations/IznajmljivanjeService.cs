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
    public async Task IznajmiVoziloAsync(int brDana, string regBroj, string jmbg)
    {
        var korisnik = await _korisnikRepo.PrikaziKorisnikaAsync(jmbg);
        var vozilo = await _voziloRepo.PrikaziVoziloAsync(regBroj);

        vozilo!.Korisnik = korisnik;
        vozilo.Iznajmljen = true;
        vozilo.BrDanaIznajmljivanja = brDana;
        korisnik!.Vozila!.Add(vozilo);

        await _unitOfWork.CommitAsync();
    }


    //HELPERI
    public async Task<bool> DaLiKorisnikPostojiAsync(string jmbg)
    {
        return await _korisnikRepo.DaLiPostojiAsync(jmbg);
    }

    public async Task<bool> DaLiVoziloPostojiAsync(string regBroj)
    {
        return await _voziloRepo.DaLiPostojiAsync(regBroj);
    }

    public async Task<bool> DaLiJeVoziloIznajmljeno(string regBroj)
    {
        return await _voziloRepo.DaLiJeIznajmljeno(regBroj);

    }
}
