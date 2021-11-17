using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UltimateMovieNight.Model;
using UltimateMovieNight.Repository;
using UltimateMovieNight.Repository.Implementation;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UltimateMovieNight.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieRepository _repository;

        public MovieController(ILogger<MovieController> logger, IMovieRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Movie>> FindAll() =>
            _repository.FindAll();

        [HttpGet("{id:length(24)}", Name = "FindMovieById")]
        public ActionResult<Movie> FindById(string id)
        {
            var movie = _repository.FindById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost]
        public ActionResult<Movie> Create(Movie movieIn)
        {
            _repository.Create(movieIn);
            return CreatedAtRoute("FindMovieById", new { id = movieIn.Id }, movieIn);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Movie movieIn)
        {
            var movie = _repository.FindById(id);

            if (movie == null)
            {
                return NotFound();
            }

            _repository.Update(id, movieIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteById(string id)
        {
            var movie = _repository.FindById(id);

            if (movie == null)
            {
                return NotFound();
            }

            _repository.DeleteById(id);

            return NoContent();
        }

            
    }
}

