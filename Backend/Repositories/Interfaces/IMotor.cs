namespace WebTemplate.Repositories.Interfaces;

public interface IMotor : IDisposable
{
    public Task<ActionResult> DodajAsync([FromBody] Motor motor);
    public Task<ActionResult> PrikaziSveMarkeAsync();
}