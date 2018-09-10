using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRider.Data;
using MovieRider.Dtos;
using MovieRider.Models;

namespace MovieRider.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET /api/movies
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {

            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }

            return moviesQuery
                .ToList()
                .Select(_mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movies/1
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(UriHelper.GetDisplayUrl(Request) + "/" + movie.Id), movieDto);
        }

        //PUT /api/movies/1
        [HttpPut("{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Bad request");

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new Exception("Not Found");

            _mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();
        }

        //DELETE /api/movies/1
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void DeteleMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new Exception("Not Found");

            _context.Movies.Remove(movieInDb);

            _context.SaveChanges();
        }
    }
}