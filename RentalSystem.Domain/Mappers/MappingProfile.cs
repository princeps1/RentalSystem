using RentalSystem.Domain.DTOs.Auth;

namespace RentalSystem.Domain.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        //AUTH
        CreateMap<ApplicationUser, UserDTO>().ReverseMap();


        //KORISNIK
        CreateMap<Korisnik, KorisnikDTO>()
        .ForMember(dest => dest.Vozila, opt => opt.MapFrom(src => src.Vozila))
        .ReverseMap();



        //VOZILO
        CreateMap<Vozilo, VoziloMinimumDTO>().ReverseMap();
        CreateMap<Vozilo, VoziloDTO>()
       .ForMember(dest => dest.ImePrezimeKorisnika,
                       opt => opt.MapFrom(src => src.Korisnik != null ? src.Korisnik.ImePrezime : "N/A"));


        //AUTOMOBIL I MOTOR
        CreateMap<MotorDodavanjeDTO, Motor>();
        CreateMap<AutomobilDodavanjeDTO, Automobil>();

       

    }
}
