using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Joker.Models
{
    public class Joke
    {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("jokeId")]
        public long JokeId { get; set; }
        [BsonElement("content")]
        public string Content { get; set; }
        [BsonElement("descrption")]
        public string Description { get; set; }
        [BsonElement("addedDate")]
        public DateTime AddedDate { get; set; }
    }
}
