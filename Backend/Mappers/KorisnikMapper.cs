using AutoMapper;


namespace RentalSystem.Mappers;

public class KorisnikMapper : Profile
{
    public KorisnikMapper()
    {
        CreateMap<Korisnik, KorisnikDTO>()
            .ForMember(dest => dest.Vozila, opt => opt.MapFrom(src => src.Vozila)); // Mapiranje liste vozila
    }
}

