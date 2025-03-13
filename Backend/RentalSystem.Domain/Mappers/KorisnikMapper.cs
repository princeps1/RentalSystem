using RentalSystem.Domain.DTOs.Auth;

namespace RentalSystem.Domain.Mappers;

public class KorisnikMapper : Profile
{
    public KorisnikMapper()
    {
        CreateMap<Korisnik, KorisnikDTO>()
            .ForMember(dest => dest.Vozila, opt => opt.MapFrom(src => src.Vozila)); // Mapiranje liste vozila
        
        CreateMap<ApplicationUser,UserDTO>().ReverseMap();  
    }
}

