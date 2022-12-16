using FilmsWebApi.Models;
using FilmsWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FilmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : Controller
    {
            private readonly FilmsDbContext _dbContext;
            public FilmController(IMyDependency context)
            {
                _dbContext = context.GetContext();
            }

            [HttpGet]
            public IActionResult GetFilms()
            {
                return Ok(_dbContext.Films.ToList());
            }

            [HttpGet]
            [Route("{id:int}")]
            public async Task<IActionResult> GetFilm([FromRoute] int id)
            {
                var film = await _dbContext.Films.FindAsync(id);
                if (film == null)
                {
                    return NotFound();
                }
                return Ok(film);
            }

            [HttpPost]
            public async Task<IActionResult> AddProduct(AddFilmRequest addFilmRequest)
            {
            //not adding id because it`s automatically generated in sql
                var film = new Film()
                {
                    Link = addFilmRequest.Link
                };
                await _dbContext.Films.AddAsync(film);
                await _dbContext.SaveChangesAsync();
                return Ok(film);
            }

            [HttpPut]
            [Route("{id:int}")]
            public async Task<IActionResult> UpdateFilm([FromRoute] int id, UpdateFilmRequest updateFilmRequest)
            {
                var film = await _dbContext.Films.FindAsync(id);

                if (film != null)
                {
                    film.Link = updateFilmRequest.Link;

                    await _dbContext.SaveChangesAsync();

                    return StatusCode(StatusCodes.Status201Created, "Film has been succesfuly changed");
                }
                else return StatusCode(StatusCodes.Status400BadRequest, "Error while updating film");
            }

            [HttpDelete]
            [Route("{id:int}")]
            public async Task<IActionResult> DeleteFilm([FromRoute] int id)
            {
                var film = await _dbContext.Films.FindAsync(id);
                if (film != null)
                {
                    _dbContext.Films.Remove(film);
                    await _dbContext.SaveChangesAsync();
                    return Ok(film);
                }
                return NotFound();
            }

    }
}
