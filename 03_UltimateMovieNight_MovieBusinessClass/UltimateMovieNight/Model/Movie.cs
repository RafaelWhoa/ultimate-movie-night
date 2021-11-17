using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UltimateMovieNight.Model
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string MovieName { get; set; }

        public string Genre { get; set; }

        public int ReleaseYear { get; set; }

        public string AvailableAt { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Watched { get; set; }
    }
}

