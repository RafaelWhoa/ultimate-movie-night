using System;
using MongoDB.Driver;
using UltimateMovieNight.Contracts;
using UltimateMovieNight.Model;
using UltimateMovieNight.Repository;

namespace UltimateMovieNight.Business.Implementation
{
    public class MovieBusiness : IMovieBusiness
    {
        private readonly IMovieRepository _repository;

        public MovieBusiness(IMovieRepository repository)
        {
            _repository = repository;
        }

        public List<Movie> FindAll() =>
            _repository.FindAll();

        public Movie FindById(string id) =>
            _repository.FindById(id);

        public Movie FindRandom() =>
            _repository.FindRandom();

        public Movie FindRandomWithFilter(MovieQuery query) =>
            _repository.FindRandomWithFilter(query);

        public Movie Create(Movie movieIn) =>
            _repository.Create(movieIn); 

        public void Update(string id, Movie movieIn) =>
            _repository.Update(id, movieIn);

        public void DeleteById(string id) =>
            _repository.DeleteById(id);
    }
}

