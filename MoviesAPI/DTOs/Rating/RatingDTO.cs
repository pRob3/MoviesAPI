using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs.Rating
{
    public class RatingDTO
    {
        [Range(1,5)]
        public int Rating { get; set; }

        public int MovieId { get; set; }
    }
}
