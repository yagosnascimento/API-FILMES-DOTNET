using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody]Filme filme)
    {
        filme.Id = filmes.Count;
        filmes.Add(filme);
        return CreatedAtAction(nameof(FilmePorId), new { id = filme.Id}, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> ObterFilmes([FromQuery]int skip = 0, [FromQuery]int take = 20) // Exemplo: /filme?skip=0&take=10
    {
        return filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult FilmePorId(int id)
    {
        foreach (Filme filme in filmes)
        {
            if (filme.Id == id) return Ok(filme);
        }
        return NotFound();
    }
}
