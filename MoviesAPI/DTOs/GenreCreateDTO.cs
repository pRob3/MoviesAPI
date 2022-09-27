using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class GenreCreateDTO
    {

        [Required]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = string.Empty;
    }
}
