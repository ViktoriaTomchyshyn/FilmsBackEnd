using System;
using System.Collections.Generic;

namespace FilmsWebApi.Models;

public partial class Recommendation
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    public int RecomendedFilmId { get; set; }
}
