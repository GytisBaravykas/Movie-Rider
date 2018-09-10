using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRider.Models;

namespace MovieRider.ViewModel
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Movie> Customers { get; set; }
    }
}
