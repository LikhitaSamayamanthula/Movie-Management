using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MovieManagment.Application
{
    public class Movie
    {
        public Movie(int movieId, string title, string genre, int releaseYear, string director)
        {
            MovieId = movieId;
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            Director = director;
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
    }
} 

