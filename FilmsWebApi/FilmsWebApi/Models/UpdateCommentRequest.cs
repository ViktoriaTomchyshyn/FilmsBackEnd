namespace FilmsWebApi.Models
{
    public class UpdateCommentRequest
    {
        public int FilmId { get; set; }

        public string? Comment1 { get; set; }
    }
}
