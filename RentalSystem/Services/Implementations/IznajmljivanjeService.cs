using AutoMapper;
using RentalSystem.Domain.Models;

namespace RentalSystem.Services.Implementations;

public class IznajmljivanjeService : IIznajmljivanjeService
{
    private readonly IVoziloRepository _voziloRepo;
    private readonly IKorisnikRepository _korisnikRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public IznajmljivanjeService(IVoziloRepository voziloRepo, IKorisnikRepository korisnikRepo, IUnitOfWork unitOfWork,IMapper mapper)
    {
        _voziloRepo = voziloRepo;
        _korisnikRepo = korisnikRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task IznajmiVoziloAsync(int brDana, string regBroj, string jmbg)
    {
        var korisnikDTO = await _korisnikRepo.PrikaziKorisnikaAsync(jmbg);
        var vozilo = await _voziloRepo.PrikaziVoziloAsync(regBroj);

        //List<KorisnikDTO> korisniciDTO = _mapper.Map<List<KorisnikDTO>>(korisnici);
        Korisnik korisnik = _mapper.Map<Korisnik>(korisnikDTO);
        //Vozilo vozilo = _mapper.Map<Vozilo>(voziloDTO);

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
