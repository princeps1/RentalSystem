using Microsoft.AspNetCore.Mvc;
using Refit;
using RentalSystemUI.DTOs.Korisnik;

namespace RentalSystemUI.DataAccess;
//https://localhost:7000/api
public interface IKorisnik
{
    [Get("/Korisnik/PrikaziSve")]
    public Task<List<KorisnikDTO>> PrikaziSve();
}
