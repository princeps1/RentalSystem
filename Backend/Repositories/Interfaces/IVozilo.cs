namespace WebTemplate.Repositories.Interfaces;

public interface IVozilo : IDisposable
{

    public Task<ActionResult> DodajAsync(Vozilo vozilo);
    public Task<ActionResult> PrikaziSveAsync();
    public Task<ActionResult> IznajmiAsync(string imePrezime, string jmbg, int brVozacke, int brIznajmljivanja, int idVozila);
    public Task<ActionResult> ObrisiAsync(int id);

}