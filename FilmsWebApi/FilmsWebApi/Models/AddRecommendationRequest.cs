namespace FilmsWebApi.Models
{
    public class AddRecommendationRequest
    {
        public int FilmId { get; set; }

        public int RecomendedFilmId { get; set; }
    }
}
