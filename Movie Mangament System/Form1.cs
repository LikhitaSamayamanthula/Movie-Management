using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Epam.MovieManagment.Application;
using Newtonsoft.Json;

namespace Movie_Mangament_System
{
    public partial class Form1 : Form
    {
        private readonly MovieService _movieService = new MovieService();
        
        private List<Movie> _movies = new List<Movie>();
        private int _currentIndex = -1;

        public Form1()
        {
            InitializeComponent();
            _movieService.LoadMovies();
            LoadMovies();
        }


        private void LoadMovies()
        {
            _movies = _movieService.GetAllMovies(); 

            if (_movies.Count > 0)
            {
                _currentIndex = 0; 
                DisplayMovie(_currentIndex);
            }

       
            ShowMovies();
        }

       
        private void ShowMovies()
        {
            StringBuilder movieText = new StringBuilder();

            foreach (var movie in _movies)
            {
                movieText.AppendLine($"ID: {movie.MovieId}");
                movieText.AppendLine($"Title: {movie.Title}");
                movieText.AppendLine($"Genre: {movie.Genre}");
                movieText.AppendLine($"Release Year: {movie.ReleaseYear}");
                movieText.AppendLine($"Director: {movie.Director}");
                movieText.AppendLine("--------------------------------------"); 
            }

            ShowList.Text = movieText.ToString(); 
        }




       
       





        private void DisplayMovie(int index)
        {
            if (index >= 0 && index < _movies.Count)
            {
                _currentIndex = index;
                Movie movie = _movies[index];

                ShowList.Text = $"ID: {movie.MovieId}\r\n" +
                                $"Title: {movie.Title}\r\n" +
                                $"Genre: {movie.Genre}\r\n" +
                                $"Release Year: {movie.ReleaseYear}\r\n" +
                                $"Director: {movie.Director}";
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMovieID.Text.Trim(), out int movieId))
                {
                    MessageBox.Show("Invalid Movie ID. Please enter a valid number.");
                    return;
                }

                if (!int.TryParse(txtYear.Text.Trim(), out int releaseYear))
                {
                    MessageBox.Show("Invalid Release Year.");
                    return;
                }

                var movie = new Movie(movieId, txtTitle.Text, txtGenre.Text, releaseYear, txtDirector.Text);
                _movieService.Add(movie);
                MessageBox.Show("Movie added successfully!");

                LoadMovies();
                _currentIndex = _movies.Count - 1; 
                DisplayMovie(_currentIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMovieID.Text.Trim(), out int movieId) ||
                    !int.TryParse(txtYear.Text.Trim(), out int releaseYear))
                {
                    MessageBox.Show("Invalid input. Check Movie ID and Release Year.");
                    return;
                }

                var movie = new Movie(movieId, txtTitle.Text, txtGenre.Text, releaseYear, txtDirector.Text);

                bool updated = _movieService.Update(movie); 

                if (updated)
                {
                    _movieService.SaveMovies(); 
                    MessageBox.Show("Movie updated successfully!");

                    LoadMovies(); 
                    DisplayMovie(_movies.FindIndex(m => m.MovieId == movieId)); 
                }
                else
                {
                    MessageBox.Show("Movie not found. Update failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentIndex >= 0 && _currentIndex < _movies.Count)
                {
                    int movieId = _movies[_currentIndex].MovieId;  

                    _movieService.Delete(movieId); 
                    _movieService.SaveMovies(); 

                    MessageBox.Show("Movie deleted successfully!");

                    LoadMovies(); 
                }
                else
                {
                    MessageBox.Show("No movie selected!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }







        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                _movieService.SaveMovies();
                MessageBox.Show("Movies saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnfirst_Click(object sender, EventArgs e)
        {
            if (_movies.Count > 0)
            {
                _currentIndex = 0; 
                DisplayMovie(_currentIndex);
            }
            else
            {
                MessageBox.Show("No movies available.");
            }
        }


        private void btnLast_Click(object sender, EventArgs e)
        {
            if (_movies.Count > 0)
            {
                _currentIndex = _movies.Count - 1;
                DisplayMovie(_currentIndex);
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _movies.Count - 1)
            {
                _currentIndex++;
                DisplayMovie(_currentIndex);
            }
            else
            {
                MessageBox.Show("No more movies.");
            }
        }

        private void btnprevious_Click(object sender, EventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                DisplayMovie(_currentIndex);
            }
            else
            {
                MessageBox.Show("Already at the first movie.");
            }
        }


        private void btnload_Click(object sender, EventArgs e)
        {
            try
            {
                _movieService.LoadMovies();
                LoadMovies();
                MessageBox.Show("Movies loaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


    }
}