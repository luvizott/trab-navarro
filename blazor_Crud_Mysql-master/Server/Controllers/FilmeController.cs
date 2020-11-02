using blazor_mysql2.Server;
using Microsoft.AspNetCore.Mvc;
using blazor_mysql2.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class FilmeController : Controller
{
    private readonly AppDbContext db;

    public FilmeController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var filmes = await db.Filmes.ToListAsync();
        return Ok(filmes);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        var filme = await db.Filmes.SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
        return Ok(filme);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Post([FromBody] Filme filme)
    {
        try
        {
            var newFilme = new Filme
            {
                Genero = filme.Genero,
                Name = filme.Name,
                Date = filme.Date,
            };

            db.Add(newFilme);
            await db.SaveChangesAsync();//INSERT INTO
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Put([FromBody] Filme filme)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.Entry(filme).State = EntityState.Modified;
        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw (ex);
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult<Filme>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var filme = await db.Filmes.FindAsync(id);
        if (filme == null)
        {
            return NotFound();
        }
        db.Filmes.Remove(filme);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private bool FilmeExists(int id)
    {
        return db.Filmes.Any(e => e.Id == id);
    }

}