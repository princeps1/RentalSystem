namespace WebTemplate.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public IKorisnikRepository KorisnikRepository { get; }
    public IVoziloRepository VoziloRepository { get; }

    public UnitOfWork(Context context, IKorisnikRepository korisnikRepo, IVoziloRepository voziloRepo)
    {
        _context = context;
        KorisnikRepository = korisnikRepo;
        VoziloRepository = voziloRepo;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
