using AutoMapper;

namespace WebTemplate.Mappers;

public class VoziloMapper : Profile
{
    public VoziloMapper()
    {
        CreateMap<Vozilo, VoziloMinimumDTO>();

        CreateMap<MotorDodavanjeDTO, Motor>();
        CreateMap<AutomobilDodavanjeDTO, Automobil>();

        CreateMap<Vozilo, VoziloDTO>()
       .ForMember(dest => dest.ImePrezimeKorisnika,
                       opt => opt.MapFrom(src => src.Korisnik != null ? src.Korisnik.ImePrezime : "N/A"));
    }
}
