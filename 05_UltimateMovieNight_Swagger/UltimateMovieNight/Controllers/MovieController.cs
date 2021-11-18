using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UltimateMovieNight.Business;
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
        private readonly IMovieBusiness _movieBusiness;

        public MovieController(ILogger<MovieController> logger, IMovieBusiness movieBusiness)
        {
            _logger = logger;
            _movieBusiness = movieBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<Movie>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public ActionResult<List<Movie>> FindAll() =>
            _movieBusiness.FindAll();

        [HttpGet("{id:length(24)}", Name = "FindMovieById")]
        [ProducesResponseType((200), Type = typeof(Movie))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public ActionResult<Movie> FindById(string id)
        {
            var movie = _movieBusiness.FindById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(Movie))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public ActionResult<Movie> Create(Movie movieIn)
        {
            _movieBusiness.Create(movieIn);
            return CreatedAtRoute("FindMovieById", new { id = movieIn.Id }, movieIn);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType((200), Type = typeof(Movie))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Update(string id, Movie movieIn)
        {
            var movie = _movieBusiness.FindById(id);

            if (movie == null)
            {
                return NotFound();
            }

            _movieBusiness.Update(id, movieIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult DeleteById(string id)
        {
            var movie = _movieBusiness.FindById(id);

            if (movie == null)
            {
                return NotFound();
            }

            _movieBusiness.DeleteById(id);

            return NoContent();
        }

            
    }
}

