using System;
using UltimateMovieNight.Model;

namespace UltimateMovieNight.Repository
{
    public interface IMovieRepository
    {
        List<Movie> FindAll();
        Movie FindById(string id);
        Movie Create(Movie movieIn);
        void Update(string id, Movie movieIn);
        void DeleteById(string id);

    }
}

