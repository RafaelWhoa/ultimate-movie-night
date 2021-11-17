using System;
using MongoDB.Driver;
using UltimateMovieNight.Model;

namespace UltimateMovieNight.Repository.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieRepository(IMovieNightDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _movies = database.GetCollection<Movie>(settings.MoviesCollectionName);
        }

        public List<Movie> FindAll() =>
            _movies.Find(movie => true).ToList();

        public Movie FindById(string id) =>
            _movies.Find(movie => movie.Id == id).FirstOrDefault();

        public Movie Create(Movie movieIn)
        {
            movieIn.AddedAt = DateTime.Now;
            movieIn.UpdatedAt = DateTime.Now;
            _movies.InsertOne(movieIn);
            return movieIn;
        }

        public void Update(string id, Movie movieIn)
        {
            movieIn.UpdatedAt = DateTime.Now;
            _movies.ReplaceOne(movie => movie.Id == id, movieIn);
        }

        public void DeleteById(string id)
        {
            _movies.DeleteOne(movie => movie.Id == id);
        }
    }
}

