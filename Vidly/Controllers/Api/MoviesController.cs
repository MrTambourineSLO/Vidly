using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
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
        public IHttpActionResult GetMovies()
        {
            var movieDtos = _context.Movies.Include(i => i.Genre).ToList().Select(Mapper.Map<Movie,MovieDto>);
            return Ok(movieDtos);
        } 
        //GET
        // /api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movieFromDb = _context.Movies.Single(m => m.Id == id);
            if (movieFromDb == null)
            {
                return NotFound();
            }
            var movie = Mapper.Map<Movie, MovieDto>(movieFromDb);
            return Ok(movie);
        }
        //POST
        // /api/movies/
        [System.Web.Http.Authorize(Roles = RoleName.CanManageMovies)]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //Kreiraj nov movie iz DTO
            var newMovie = Mapper.Map<MovieDto, Movie>(movieDto);
            //Dodaj NumberAvailable = NumberInStock
            newMovie.NumberAvailable = newMovie.NumberInStock;
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            movieDto.Id = newMovie.Id;
            
            return Created(new Uri(Request.RequestUri +"/"+movieDto.Id ),movieDto );
        }
        //PUT
        // /api/movies/id
        [System.Web.Http.Authorize(Roles = RoleName.CanManageMovies)]
        [System.Web.Http.HttpPut]
        public IHttpActionResult EditMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movieInDb = _context.Movies.Single(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }
            
            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }
        //DELETE
        // /api/movies/id
        [System.Web.Http.Authorize(Roles = RoleName.CanManageMovies)]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == id);
            if (movieInDb == null)
            {
                //return error
            }
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
