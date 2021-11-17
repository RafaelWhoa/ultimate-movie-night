using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UltimateMovieNight.Model
{
    [RuntimeVersion("1.0.0")]
    public class Movie : IDocument
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

        public DocumentVersion Version { get; set; }
        public string Type { get; internal set; }
    }
}

