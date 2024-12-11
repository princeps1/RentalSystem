namespace WebTemplate.Repositories.Interfaces;

public interface IKorisnik : IDisposable
{
    public Task<ActionResult> ObrisiAsync(int id);
}