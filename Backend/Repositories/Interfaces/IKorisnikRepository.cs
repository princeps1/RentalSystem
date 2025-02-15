namespace WebTemplate.Repositories.Interfaces;

public interface IKorisnikRepository
{
    public Task<ActionResult> DodajAsync(string imePrezime, string jmbg, int brVozacke);
    public Task<ActionResult> AzurirajVozackuAsync(int id,int brVozacke);
    public Task<ActionResult> PrikaziSveAsync();
    public Task<Korisnik?> PrikaziKorisnikaAsync(int id);
    public Task<ActionResult> ObrisiAsync(int id);

}