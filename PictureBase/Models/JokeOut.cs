using System;

namespace PictureBase.Models
{
    public class JokeOut
    {
        public string Id { get; set; }

        public long JokeId { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTime AddedDate { get; set; }

        public string Rate { get; set; }
    }
}
