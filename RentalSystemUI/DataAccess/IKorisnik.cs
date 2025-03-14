using RentalSystemUI.DTOs;
using Refit;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using RentalSystemUI.DTOs.Korisnik;

namespace RentalSystemUI.DataAccess;

public interface IKorisnik
{
    [Post("/Korisnik/Dodaj")]
    Task Dodaj([Query, Required] string imePrezime,[Query, Required, StringLength(13)] string jmbg,
                              [Query, Required] string brVozacke,[Header("Authorization")] string token);

    [Post("/Korisnik/AzurirajVozacku")]
    public Task<IActionResult> AzurirajVozacku([FromQuery, Required, StringLength(13)] string jmbg,
                                               [FromQuery, Required] string noviBrVozacke, [Header("Authorization")] string token);


    [Get("/Korisnik/PrikaziSve")]
    Task<List<KorisnikDTO>> PrikaziSve([Header("Authorization")] string token);


    [Get("/Korisnik/PrikaziKorisnika")]
    public Task<IActionResult> PrikaziKorisnika([FromQuery, Required, StringLength(13)] string jmbg, string token);

    [Delete("/Korisnik/Obrisi")]
    public Task<IActionResult> Obris([FromQuery, Required, StringLength(13)] string jmbg, string token);

}
