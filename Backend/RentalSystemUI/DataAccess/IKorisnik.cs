using RentalSystemUI.DTOs;
using Refit;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using RentalSystemUI.DTOs.Korisnik;

namespace RentalSystemUI.DataAccess;

public interface IKorisnik
{
    [Post("/Korisnik/Dodaj")]
    public Task<IActionResult> Dodaj([FromQuery, Required] string imePrezime,
                                           [FromQuery, Required, StringLength(13)] string jmbg,
                                           [FromQuery, Required] string brVozacke, string token);

    [Post("/Korisnik/AzurirajVozacku")]
    public Task<IActionResult> AzurirajVozacku([FromQuery, Required, StringLength(13)] string jmbg,
                                                         [FromQuery, Required] string noviBrVozacke, string token);


    [Get("/Korisnik/PrikaziSve")]
    Task<List<KorisnikDTO>> PrikaziSve([Header("Authorization")] string token);


    [Get("/Korisnik/PrikaziKorisnika")]
    public Task<IActionResult> PrikaziKorisnika([FromQuery, Required, StringLength(13)] string jmbg, string token);

    [Delete("/Korisnik/Obrisi")]
    public Task<IActionResult> Obris([FromQuery, Required, StringLength(13)] string jmbg, string token);

}
