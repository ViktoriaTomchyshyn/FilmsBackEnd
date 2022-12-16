using FilmsWebApi.Models;
using FilmsWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : Controller
    {
        private readonly FilmsDbContext _dbContext;
        public RecommendationController(IMyDependency context)
        {
            _dbContext = context.GetContext();
        }

        [HttpGet]
        public IActionResult GetRecommendations()
        {
            return Ok(_dbContext.Recommendations.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetRecommendation([FromRoute] int id)
        {
            var recommendation = await _dbContext.Recommendations.FindAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }
            return Ok(recommendation);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecommendation(AddRecommendationRequest addRecommendationRequest)
        {

            if (!_dbContext.Films.Select(v => v.Id).Distinct().ToList().Contains((int)addRecommendationRequest.FilmId)
                || !_dbContext.Films.Select(v => v.Id).Distinct().ToList().Contains((int)addRecommendationRequest.RecomendedFilmId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Enter IDs from available in table Films");
            }
            //not adding id because it`s automatically generated in sql
            var recommendation = new Recommendation()
            {
                FilmId = addRecommendationRequest.FilmId,
                RecomendedFilmId = addRecommendationRequest.RecomendedFilmId
            };
            await _dbContext.Recommendations.AddAsync(recommendation);
            await _dbContext.SaveChangesAsync();
            return Ok(recommendation);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateRecommendation([FromRoute] int id, UpdateRecommendationRequest updateRecommendationRequest)
        {
            if (!_dbContext.Films.Select(v => v.Id).Distinct().ToList().Contains((int)updateRecommendationRequest.FilmId)
                || !_dbContext.Films.Select(v => v.Id).Distinct().ToList().Contains((int)updateRecommendationRequest.RecomendedFilmId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Enter IDs from available in table Films");
            }

            var recommendation = await _dbContext.Recommendations.FindAsync(id);

            if (recommendation != null)
            {
                recommendation.FilmId = updateRecommendationRequest.FilmId;
                recommendation.RecomendedFilmId = updateRecommendationRequest.RecomendedFilmId;

                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, "Recommendation has been succesfuly changed");
            }
            else return StatusCode(StatusCodes.Status400BadRequest, "Error while updating recommendation");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteRecommendation([FromRoute] int id)
        {
            var recommendation = await _dbContext.Recommendations.FindAsync(id);
            if (recommendation != null)
            {
                _dbContext.Recommendations.Remove(recommendation);
                await _dbContext.SaveChangesAsync();
                return Ok(recommendation);
            }
            return NotFound();
        }
    }
}
