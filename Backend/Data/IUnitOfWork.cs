using WebTemplate.Repositories.Implementations;

public interface IUnitOfWork : IDisposable
{
    IKorisnikRepository KorisnikRepository { get; }
    IVoziloRepository VoziloRepository { get; }

    Task<int> CommitAsync();
}
