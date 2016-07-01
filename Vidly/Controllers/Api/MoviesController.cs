using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Vidly.DTOs;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public MoviesController()
        {
           _context = new ApplicationDbContext();
        }
        //GET
        // /api/movies/
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDto>);
        } 
        //GET
        // /api/movies/id
        public MovieDto GetMovie(int id)
        {
            var movieFromDb = _context.Movies.Single(m => m.Id == id);
            if (movieFromDb == null)
            {
                //
            }
            var movie = Mapper.Map<Movie, MovieDto>(movieFromDb);
            return movie;
        }
        //POST
        // /api/movies/
        public MovieDto EditMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                //Return error
            }
            //Kreiraj nov movie iz DTO
            var newMovie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            movieDto.Id = newMovie.Id;
            return movieDto;
        }
        //PUT
        // /api/movies/id
        public void EditMovie(int id, MovieDto movieDto)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == id);
            if (movieInDb == null)
            {
                //return error
            }
            
            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            _context.SaveChanges();

        }
        //DELETE
        // /api/movies/id
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == id);
            if (movieInDb == null)
            {
                //return error
            }
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
