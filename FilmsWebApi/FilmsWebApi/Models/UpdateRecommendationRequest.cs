namespace FilmsWebApi.Models
{
    public class UpdateRecommendationRequest
    {
        public int FilmId { get; set; }

        public int RecomendedFilmId { get; set; }
    }
}
