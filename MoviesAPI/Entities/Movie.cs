using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 75)]
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Trailer { get; set; } = string.Empty;
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; } = string.Empty;

        public List<MoviesGenres> MoviesGenres { get; set; }
        public List<MovieTheatersMovies> MovieTheatersMovies { get; set; }
        public List<MoviesActors> MoviesActors { get; set; }
    }
}
