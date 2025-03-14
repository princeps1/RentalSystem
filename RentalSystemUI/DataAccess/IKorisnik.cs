using RentalSystemUI.DTOs;
using Refit;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using RentalSystemUI.DTOs.Korisnik;
using System.Xml.Linq;

namespace RentalSystemUI.DataAccess;

public interface IKorisnik
{
    [Post("/Korisnik/Dodaj")]
    Task Dodaj([Query, Required] string imePrezime,[Query, Required, StringLength(13)] string jmbg,[Query, Required] string brVozacke,[Header("Authorization")] string token);

    [Put("/Korisnik/Azuriraj")]
    Task<string> AzurirajVozacku([AliasAs("jmbg")] string jmbg,[AliasAs("noviBrVozacke")] string noviBrVozacke,[Header("Authorization")] string token);

    [Get("/Korisnik/PrikaziSve")]
    Task<List<KorisnikDTO>> PrikaziSve([Header("Authorization")] string token);

    [Get("/Korisnik/PrikaziKorisnika/{jmbg}")]
    Task<KorisnikDTO> PrikaziKorisnika([AliasAs("jmbg")] string jmbg, [Header("Authorization")] string token);

    [Delete("/Korisnik/Obrisi/{jmbg}")]
    public Task Obris([AliasAs("jmbg")] string jmbg, [Header("Authorization")] string token);

}
