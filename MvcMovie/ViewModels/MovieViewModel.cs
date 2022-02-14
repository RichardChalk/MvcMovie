using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        // The user isn't required to enter time information in the date field.
        // Only the date is displayed, not time information
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        
        public string? Genre { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}