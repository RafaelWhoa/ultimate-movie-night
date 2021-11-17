namespace UltimateMovieNight.Model
{
    public interface IMovieNightDatabaseSettings
    {
        string MoviesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

