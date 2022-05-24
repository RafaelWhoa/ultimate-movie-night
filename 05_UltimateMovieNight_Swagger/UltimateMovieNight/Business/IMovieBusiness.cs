using System;
using UltimateMovieNight.Contracts;
using UltimateMovieNight.Model;

namespace UltimateMovieNight.Business
{
    public interface IMovieBusiness
    {
        List<Movie> FindAll();
        Movie FindById(string id);
        Movie FindRandom();
        Movie FindRandomWithFilter(MovieQuery query);
        Movie Create(Movie movieIn);
        void Update(string id, Movie movieIn);
        void DeleteById(string id);

    }
}

