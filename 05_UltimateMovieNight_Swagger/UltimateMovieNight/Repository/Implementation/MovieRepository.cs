using MongoDB.Driver;
using UltimateMovieNight.Contracts;
using UltimateMovieNight.Model;

namespace UltimateMovieNight.Repository.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movies;
        private static Random rnd = new Random();

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

        public Movie FindRandom() =>
            _movies
            .Aggregate().AppendStage<Movie>($@"{{ $sample: {{ size: 1 }} }}").Single();

        public Movie FindRandomWithFilter(MovieQuery query)
        {
            var filter = BuildFilter(query);
            var filteredMovies = GetMovies(filter).ToList();
            var shuffleMovies = filteredMovies.OrderBy(a => rnd.Next()).ToList();
            return shuffleMovies.FirstOrDefault();  
        }

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

        private IEnumerable<Movie> GetMovies(FilterDefinition<Movie> filterBuilder)
        {
            var filteredMovies = _movies.Find(filterBuilder).ToList();

            return filteredMovies;
        }

        private static FilterDefinition<Movie> BuildFilter(MovieQuery query)
        {
            //_logger.LogInformation("Building filter for {query}", query);

            var filterBuilder = Builders<Movie>.Filter.Empty;
            var builder = Builders<Movie>.Filter;

            if (query.isAAPLTVChecked)
            {
                filterBuilder &= builder.Where(mv => mv.AvailableAt.ToUpper().Contains("AAPLTV"));
            }

            if (query.isDISPChecked)
            {
                filterBuilder |= builder.Where(mv => mv.AvailableAt.ToUpper().Contains("DISP"));
            }

            if (query.isHBOChecked)
            {
                filterBuilder |= builder.Where(mv => mv.AvailableAt.ToUpper().Contains("HBO"));
            }

            if (query.isNFLXChecked)
            {
                filterBuilder |= builder.Where(mv => mv.AvailableAt.ToUpper().Contains("NFLX"));
            }

            if (query.isAMZNChecked)
            {
                filterBuilder |= builder.Where(mv => mv.AvailableAt.ToUpper().Contains("AMZN"));
            }

            if (query.isOutrosChecked)
            {
                filterBuilder |= builder.Where(mv => mv.AvailableAt.ToUpper().Contains("Outros"));
            }

            return filterBuilder;
        }
    }
}

