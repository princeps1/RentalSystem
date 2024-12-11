
namespace WebTemplate.Repositories.Interfaces;

public interface IAutomobil : IDisposable
{
    public Task<ActionResult> DodajAsync([FromBody] Automobil automobil);
    public Task<ActionResult> PrikaziSveMarkeAsync();
}
