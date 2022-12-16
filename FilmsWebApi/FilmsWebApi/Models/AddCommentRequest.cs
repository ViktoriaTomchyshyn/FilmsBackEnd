namespace FilmsWebApi.Models
{
    public class AddCommentRequest
    {
        public int FilmId { get; set; }

        public string? Comment1 { get; set; }
    }
}
