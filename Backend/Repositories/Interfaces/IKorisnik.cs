namespace WebTemplate.Repositories.Interfaces;

public interface IKorisnik : IDisposable
{
    public Task<ActionResult> DodajAsync(string imePrezime, string jmbg, int brVozacke, int brIznajmljivanja, Vozilo vozilo);

    public Task<ActionResult> ObrisiAsync(int id);
}