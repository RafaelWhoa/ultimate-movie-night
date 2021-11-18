using System;
using Mongo.Migration.Migrations.Database;
using MongoDB.Driver;
using UltimateMovieNight.Model;

namespace UltimateMovieNight.DB.Migrations
{
    public class M100_AddNewMovie : DatabaseMigration
    {
        public M100_AddNewMovie()
            : base("1.0.0")
        {
        }

        public override void Down(IMongoDatabase db)
        {
            
        }

        public override void Up(IMongoDatabase db)
        {
            var collection = db.GetCollection<Movie>("Movie");
            collection.InsertOne(new Movie
            {
                Id = "",
                MovieName = "A cinco passos de você",
                Genre = "Drama romântico",
                ReleaseYear = 2019,
                AvailableAt = "AppleTV",
                AddedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Watched = true
            });
        }
    }
}

