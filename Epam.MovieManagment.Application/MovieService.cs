using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Epam.MovieManagment.Application
{
    public class MovieService

    {
        private Hashtable movies = new Hashtable();
        private readonly string filePath = @"C:\Users\samayamanthula_likhi\source\repos\Movie Mangament System\Movie Mangament System\bin\Debug\movies.json";



        public void Add(Movie movie)
        {
            if (movies.ContainsKey(movie.MovieId))
            {
                throw new Exception("Movie ID already exists!");
            }
            movies.Add(movie.MovieId, movie);
        }
        public void Delete(int movieId)
        {
            if (!movies.ContainsKey(movieId))
            {
                throw new Exception("Movie not found!");
            }
            movies.Remove(movieId);
        }
        
        public bool Update(Movie updatedMovie)
        {
            if (movies.ContainsKey(updatedMovie.MovieId))
            {
                movies[updatedMovie.MovieId] = updatedMovie; 
                return true;
            }
            return false;
        }
        public List<Movie> GetAllMovies()
        {
            return movies.Values.Cast<Movie>().ToList();
        }
        public void SaveMovies()
        {
            var movieList = GetAllMovies();
            string json = JsonConvert.SerializeObject(movieList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
       
        public void LoadMovies()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var movieList = JsonConvert.DeserializeObject<List<Movie>>(json);
                if (movieList != null)
                {
                    movies.Clear();
                    foreach (var movie in movieList.OrderBy(m => m.MovieId)) 
                    {
                        movies.Add(movie.MovieId, movie);
                    }
                }
            }
        }


    }
    public static class MovieExtensions
    {
        public static List<Movie> ToMovieList(this Hashtable hashtable)
        {
            return hashtable.Values.Cast<Movie>().ToList();
        }
    }
}
