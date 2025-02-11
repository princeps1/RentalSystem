namespace WebTemplate.Repositories.Interfaces;

public interface IVoziloRepository : IDisposable
{

    public Task<ActionResult> DodajAsync(Vozilo vozilo);
    //public Task<ActionResult> IznajmiAsync(int brDana, int idVozila);
    public Task<ActionResult> PrikaziSveAsync();
    public Task<Vozilo?> PrikaziVoziloAsync(int id);

    public Task<ActionResult> ObrisiAsync(int id);

}