using Refit;
using RentalSystemUI.DTOs.Vozilo;

namespace RentalSystemUI.DataAccess;

public interface IVozilo
{
    [Get("/Vozilo/PrikaziSve")]
    Task<List<VoziloDTO>> PrikaziSve([Header("Authorization")] string token);

}
