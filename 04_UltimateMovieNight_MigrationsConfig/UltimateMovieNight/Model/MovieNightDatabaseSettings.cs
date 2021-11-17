namespace UltimateMovieNight.Model
{
    public class MovieNightDatabaseSettings : IMovieNightDatabaseSettings
    {
        public string MoviesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

