using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieRider.Models;

namespace MovieRider.ViewModel
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Required(ErrorMessage = "Date format should be year/month/day")]
        [Display(Name = "Release date")]
        public DateTime? ReleaseDate { get; set; }
        
        [Display(Name = "Number in stock")]
        [Range(1, 20,ErrorMessage = "Number must be greater than 0.")]
        [Required(ErrorMessage = "Number must be greater than 0.")]
        public byte? NumberInStock { get; set; }

        
        [Display(Name = "Number available")]
        [Range(1, 20)]
        public byte NumberAvailable { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }
        
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
            NumberAvailable = movie.NumberAvailable;

        }
    }
}
