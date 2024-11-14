using System.ComponentModel;
using System.Text.Json.Serialization;
using WebTemplate.Models;

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

    [HttpPost("DodajAutomobil")]
    public async Task<ActionResult> DodajAutomobil([FromBody] Automobil automobil)
    {
        try
        {
            await Context.Automobili.AddAsync(automobil);
            await Context.SaveChangesAsync();

            return Ok("Automobil je uspešno dodat u bazu.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajMotor")]
    public async Task<ActionResult> DodajMotor([FromBody] Motor motor)
    {
        try
        {
            await Context.Motori.AddAsync(motor);
            await Context.SaveChangesAsync();

            return Ok("Motor je uspešno dodat u bazu.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //U okviru ove funkcije se dodaje KORISNIK
    [HttpPost("Iznajmi/{imePrezime}/{jmbg}/{brVozacke}/{brIznajmljivanja}/{idVozila}")]
    public async Task<ActionResult> Iznajmi(string imePrezime, string jmbg, int brVozacke, int brIznajmljivanja, int idVozila)
    {
        //NEMA PROVERA DA LI JE VOZILO VEC IZNAJMLJENO,TO CE NA FRONT int
        try
        {
            var vozilo = Context.Vozila.Find(idVozila);
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
            return BadRequest(e.InnerException?.Message);
        }
    }


    [HttpGet("PrikaziSveModeleAutomobila")]
    public async Task<ActionResult> PrikaziSveModeleAutomobila()
    {
        try
        {
            var modeli = await Context.Automobili.Select(s => s.Model).ToListAsync();
            return Ok(modeli);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet("PrikaziSveModeleMotora")]
    public async Task<ActionResult> PrikaziSveModeleMotora()
    {
        try
        {
            var modeli = await Context.Motori.Select(s => s.Model).ToListAsync();
            return Ok(modeli);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("FiltriranjePoVozilu")]
    public async Task<ActionResult> Filtriranje(
    [FromQuery] int? kilometraza,
    [FromQuery] int? sedista,
    [FromQuery] int? cena,
    [FromQuery] string? model,
    [DefaultValue(TipVozila.Sve)] TipVozila tipVozila)
    {
        try
        {
            // Kreiraj dve liste za automobile i motore
            var automobiliQuery = Context.Automobili.AsQueryable();
            var motoriQuery = Context.Motori.AsQueryable();

            // Dodaj zajednicke filtere za oba tipa vozila
            if (kilometraza != null)
            {
                automobiliQuery = automobiliQuery.Where(s => s.PredjenoKm == kilometraza.Value);
                motoriQuery = motoriQuery.Where(s => s.PredjenoKm == kilometraza.Value);
            }

            if (cena != null)
            {
                automobiliQuery = automobiliQuery.Where(s => s.CenaVozila == cena.Value);
                motoriQuery = motoriQuery.Where(s => s.CenaVozila == cena.Value);
            }

            if (model != null)
            {
                automobiliQuery = automobiliQuery.Where(s => s.Model == model);
                motoriQuery = motoriQuery.Where(s => s.Model == model);
            }

            // Dodaj filter specifican za broj sedista samo za automobile
            if (sedista != null)
            {
                automobiliQuery = automobiliQuery.Where(s => s.BrSedista == sedista.Value);
            }


            var automobili = await automobiliQuery
                .Select(s => new
                {
                    s.Marka,
                    s.Model,
                    s.PredjenoKm,
                    s.Godiste,
                    s.BrSedista,
                    s.CenaVozila,
                    s.Iznajmljen,
                    s.Gorivo,
                    s.Karoserija
                })
                .ToListAsync();

            var motori = await motoriQuery
                .Select(s => new
                {
                    s.Marka,
                    s.Model,
                    s.PredjenoKm,
                    s.Godiste,
                    s.CenaVozila,
                    s.Iznajmljen,
                    s.Tip
                })
                .ToListAsync();

            // Primenjuj upite prema tipu vozila
            if (tipVozila == TipVozila.Automobil)
            {
                return Ok(automobili);
            }
            else if (tipVozila == TipVozila.Motor)
            {
                return Ok(motori);
            }
            else // Kada je tipVozila == TipVozila.Nista
            {
                // Kombinuj rezultate
                var svaVozila = automobili.Cast<object>().Concat(motori.Cast<object>()).ToList();

                return Ok(svaVozila);
            }
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
            var vozilo = await Context.Vozila.FindAsync(id);

            if (vozilo == null)
            {
                return NotFound("Vozilo sa zadatim ID-em nije pronađeno.");
            }

            // Brisanje entiteta
            Context.Vozila.Remove(vozilo);
            await Context.SaveChangesAsync();

            return Ok($"Vozilo sa ID-em {id} je uspešno obrisano.");
        }
        catch (Exception e)
        {
            return BadRequest($"Došlo je do greške prilikom brisanja: {e.Message}");
        }
    }


    [HttpDelete("ObrisiMotor/{id}")]
    public async Task<ActionResult> ObrisiMotor(int id)
    {
        try
        {
            // Pronalazak entiteta Motor po id-u
            var motor = await Context.Motori.FindAsync(id);

            if (motor == null)
            {
                return NotFound("Motor sa zadatim ID-em nije pronađen.");
            }

            // Brisanje entiteta Motor
            Context.Motori.Remove(motor);
            await Context.SaveChangesAsync();

            return Ok($"Motor sa ID-em {id} je uspešno obrisan.");
        }
        catch (Exception e)
        {
            return BadRequest($"Došlo je do greške prilikom brisanja: {e.Message}");
        }
    }


    [HttpDelete("ObrisiKorisnika/{id}")]
    public async Task<ActionResult> ObrisiKorisnika(int id)
    {
        try
        {
            // Pronalazak entiteta Korisnik po id-u
            var korisnik = await Context.Korisnici.FindAsync(id);

            if (korisnik == null)
            {
                return NotFound("Korisnik sa zadatim ID-em nije pronađen.");
            }

            // Brisanje entiteta Korisnik
            Context.Korisnici.Remove(korisnik);
            await Context.SaveChangesAsync();

            return Ok($"Korisnik sa ID-em {id} je uspešno obrisan.");
        }
        catch (Exception e)
        {
            return BadRequest($"Došlo je do greške prilikom brisanja: {e.Message}");
        }
    }

}
