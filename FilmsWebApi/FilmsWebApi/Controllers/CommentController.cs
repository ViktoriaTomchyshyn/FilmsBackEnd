using FilmsWebApi.Models;
using FilmsWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmsWebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly FilmsDbContext _dbContext;
        public CommentController(IMyDependency context)
        {
            _dbContext = context.GetContext();
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            return Ok(_dbContext.Comments.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await _dbContext.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentRequest addCommentRequest)
        {

            if (!_dbContext.Films.Select(v => v.Id).Distinct().ToList().Contains((int)addCommentRequest.FilmId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Enter ID from available in table Films");
            }
            //not adding id because it`s automatically generated in sql
            var comment = new Comment()
            {
                FilmId = addCommentRequest.FilmId,
                Comment1 = addCommentRequest.Comment1
            };
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentRequest updateCommentRequest)
        {
            if (!_dbContext.Films.Select(v => v.Id).Distinct().ToList().Contains((int)updateCommentRequest.FilmId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Enter ID from available in table Films");
            }

            var comment = await _dbContext.Comments.FindAsync(id);

            if (comment != null)
            {
                comment.FilmId = updateCommentRequest.FilmId;
                comment.Comment1 = updateCommentRequest.Comment1;

                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, "Comment has been succesfuly changed");
            }
            else return StatusCode(StatusCodes.Status400BadRequest, "Error while updating comment");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _dbContext.Comments.FindAsync(id);
            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return Ok(comment);
            }
            return NotFound();
        }
    }
}
