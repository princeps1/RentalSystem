using WebTemplate.Data;


namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    public Context Context { get; set; }

    public Controller(Context context)
    {
        Context = context;
    }

    //U okviru ove funkcije se dodaje KORISNIK
    [HttpPost("Iznajmi/{imePrezime}/{jmbg}/{brVozacke}/{brIznajmljivanja}/{idVozila}")]
    public async Task<ActionResult> Iznajmi(string imePrezime, string jmbg, int brVozacke, int brIznajmljivanja, int idVozila)
    {
        try
        {
            var vozilo = Context.Vozila.Find(idVozila);
            if (vozilo == null)
                return NotFound("Vozilo nije pronadjeno");
            if (vozilo.BrDanaIznajmljivanja > 0 && vozilo.Iznajmljen)
            {
                return BadRequest("Vozilo je vec iznajmljeno");
            }

            var korisnikPostoji = await Context.Korisnici.AnyAsync(k => k.BrVozacke == brVozacke || k.JMBG == jmbg);
            if (korisnikPostoji)
            {
                return BadRequest("Korisnik vec postoji");
            }


            vozilo!.BrDanaIznajmljivanja = brIznajmljivanja;
            vozilo.Iznajmljen = true;
            var korisnik = new Korisnik
            {
                ImePrezime = imePrezime,
                JMBG = jmbg,
                BrVozacke = brVozacke,
                Vozila = new List<Vozilo> { vozilo }
            };
            await Context.Korisnici.AddAsync(korisnik);
            await Context.SaveChangesAsync();
            return Ok($"Sve proslo ok \n{korisnik}");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("ObrisiVozilo/{id}")]
    public async Task<ActionResult> ObrisiVozilo(int id)
    {
        try
        {
            // Pronalazak entiteta po id-u
            var vozilo = await Context.Automobili.FindAsync(id);

            if (vozilo == null)
            {
                return NotFound("Vozilo sa zadatim ID-em nije pronađeno.");
            }

            // Brisanje entiteta
            Context.Automobili.Remove(vozilo);
            await Context.SaveChangesAsync();

            return Ok($"Vozilo sa ID-em {id} je uspešno obrisano.");
        }
        catch (Exception e)
        {
            return BadRequest($"Došlo je do greške prilikom brisanja: {e.Message}");
        }
    }





    //PITANJE DAL JE DOBRA FUNKCIJA JER NE ZNAM DA LI SU JOJ DOBRI APRAMETRI,TREBA DA SE KORISTI ZA FILTRIRANJE
    // [HttpGet("FiltriranjePoVozilu")]
    // public async Task<ActionResult> Filtriranje(
    // [FromQuery] int? kilometraza,
    // [FromQuery] int? sedista,
    // [FromQuery] int? cena,
    // [FromQuery] string? model,
    // [DefaultValue(TipVozila.Sve)] TipVozila tipVozila)
    // {
    //     try
    //     {
    //         // Kreiraj dve liste za automobile i motore
    //         var automobiliQuery = Context.Automobili.AsQueryable();
    //         var motoriQuery = Context.Motori.AsQueryable();

    //         // Dodaj zajednicke filtere za oba tipa vozila
    //         if (kilometraza != null)
    //         {
    //             automobiliQuery = automobiliQuery.Where(s => s.PredjenoKm == kilometraza.Value);
    //             motoriQuery = motoriQuery.Where(s => s.PredjenoKm == kilometraza.Value);
    //         }

    //         if (cena != null)
    //         {
    //             automobiliQuery = automobiliQuery.Where(s => s.CenaVozila == cena.Value);
    //             motoriQuery = motoriQuery.Where(s => s.CenaVozila == cena.Value);
    //         }

    //         if (model != null)
    //         {
    //             automobiliQuery = automobiliQuery.Where(s => s.Model == model);
    //             motoriQuery = motoriQuery.Where(s => s.Model == model);
    //         }

    //         // Dodaj filter specifican za broj sedista samo za automobile
    //         if (sedista != null)
    //         {
    //             automobiliQuery = automobiliQuery.Where(s => s.BrSedista == sedista.Value);
    //         }


    //         var automobili = await automobiliQuery
    //             .Select(s => new
    //             {
    //                 s.Marka,
    //                 s.Model,
    //                 s.PredjenoKm,
    //                 s.Godiste,
    //                 s.BrSedista,
    //                 s.CenaVozila,
    //                 s.Iznajmljen,
    //                 s.Gorivo,
    //                 s.Karoserija
    //             })
    //             .ToListAsync();

    //         var motori = await motoriQuery
    //             .Select(s => new
    //             {
    //                 s.Marka,
    //                 s.Model,
    //                 s.PredjenoKm,
    //                 s.Godiste,
    //                 s.CenaVozila,
    //                 s.Iznajmljen,
    //                 s.Tip
    //             })
    //             .ToListAsync();

    //         // Primenjuj upite prema tipu vozila
    //         if (tipVozila == TipVozila.Automobil)
    //         {
    //             return Ok(automobili);
    //         }
    //         else if (tipVozila == TipVozila.Motor)
    //         {
    //             return Ok(motori);
    //         }
    //         else // Kada je tipVozila == TipVozila.Nista
    //         {
    //             // Kombinuj rezultate
    //             var svaVozila = automobili.Cast<object>().Concat(motori.Cast<object>()).ToList();

    //             return Ok(svaVozila);
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
}
