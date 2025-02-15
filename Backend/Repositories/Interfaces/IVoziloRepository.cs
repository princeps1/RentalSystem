namespace WebTemplate.Repositories.Interfaces;

public interface IVoziloRepository 
{

    public Task<Vozilo> DodajAsync(Vozilo vozilo);
    public Task<List<Vozilo>> PrikaziSveAsync();
    public Task<Vozilo?> PrikaziVoziloAsync(int id);

    public Task<IActionResult> ObrisiAsync(int id);

    public Task<bool> DaLiPostojiAsync(string? regBroj = null);
}