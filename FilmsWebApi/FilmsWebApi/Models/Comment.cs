using System;
using System.Collections.Generic;

namespace FilmsWebApi.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    
    //misstyped 1 by accdent
    public string? Comment1 { get; set; }
}
