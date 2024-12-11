



namespace WebTemplate.Data;
public class Context : DbContext
{
    // DbSet kolekcije!
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Vozilo> Vozila { get; set; }
    public virtual DbSet<Automobil> Automobili { get; set; }
    public DbSet<Motor> Motori { get; set; }


    public Context(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vozilo>().ToTable("Vozila"); // Tabela za zajednicka svojstva
        modelBuilder.Entity<Automobil>().ToTable("Automobili"); // Tabela specificna za Automobil
        modelBuilder.Entity<Motor>().ToTable("Motori"); // Tabela specificna za Motor
    }

}
