using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PictureBase.Models
{
    public class Image
    {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonElement("filename")]
        public string Filename { get; set; }
        [BsonElement("descrption")]
        public string Description { get; set; }
        [BsonElement("addedDate")]
        public DateTime AddedDate { get; set; }
    }
}
