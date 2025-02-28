namespace RentalSystem.Domain.Data;
public class Context : IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Vozilo> Vozila { get; set; }
    public virtual DbSet<Automobil> Automobili { get; set; }
    public DbSet<Motor> Motori { get; set; }


    public Context(DbContextOptions<Context> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Vozilo>().ToTable("Vozila"); // Tabela za zajednicka svojstva
        modelBuilder.Entity<Automobil>().ToTable("Automobili"); // Tabela specificna za Automobil
        modelBuilder.Entity<Motor>().ToTable("Motori"); // Tabela specificna za Motor
        modelBuilder.Entity<Korisnik>().ToTable("Korisnici"); // Tabela specificna za Korisnik
    }

}
