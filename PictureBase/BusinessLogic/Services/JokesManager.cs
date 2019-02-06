using MongoDB.Bson;
using MongoDB.Driver;
using PictureBase.BusinessLogic.Contracts;
using PictureBase.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PictureBase.BusinessLogic.Services
{
    public class JokesManager : IJokesManager
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoCollection<Joke> jokesCollection;
        private readonly IDatabase redisDb;
        private readonly ConnectionMultiplexer redis;
        private const string keys = "bestJokes";

        public JokesManager(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            mongoDatabase = mongoClient.GetDatabase("jokesBase");
            jokesCollection = mongoDatabase.GetCollection<Joke>("Jokes");
            redis = ConnectionMultiplexer.Connect("localhost");
            redisDb = redis.GetDatabase();
        }

        public ServiceResponse AddJoke(JokeFromApiModel model)
        {
            var count = jokesCollection.CountDocuments(new BsonDocument());
            var joke = new Joke()
            {
                JokeId = count + 1,
                AddedDate = DateTime.Now,
                Description = model.Description,
                Content = model.Content
            };
            redisDb.SortedSetAdd(keys, joke.JokeId.ToString(), 0);
            jokesCollection.InsertOne(joke);
            return new ServiceResponse();
        }

        public List<JokeOut> GetAll()
        {
            var jokes = jokesCollection.FindSync(joke => true).ToList();
            var jokesOut = new List<JokeOut>();
            foreach (var jokeFromDb in jokes)
            {
                jokesOut.Add(new JokeOut()
                {
                    AddedDate = jokeFromDb.AddedDate,
                    Content = jokeFromDb.Content,
                    Description = jokeFromDb.Description,
                    Id = jokeFromDb.Id,
                    JokeId = jokeFromDb.JokeId,
                    Rate = redisDb.SortedSetScore(keys, jokeFromDb.JokeId.ToString()).ToString()
                });
            }

            jokesOut = jokesOut.OrderByDescending(j => j.AddedDate).ToList();

            return jokesOut;
        }

        public JokeOut GetById(string id)
        {
            var jokeFromDb = jokesCollection.Find<Joke>(joke => joke.Id == id).FirstOrDefault();
            var jokeOut = new JokeOut()
            {
                AddedDate = jokeFromDb.AddedDate,
                Content = jokeFromDb.Content,
                Description = jokeFromDb.Description,
                Id = jokeFromDb.Id,
                JokeId = jokeFromDb.JokeId,
                Rate = redisDb.SortedSetScore(keys, jokeFromDb.JokeId.ToString()).ToString()
            };
            return jokeOut;
        }

        public JokeOut GetByJokeId(string id)
        {
            var longId = long.Parse(id);
            var jokeFromDb = jokesCollection.Find<Joke>(joke => joke.JokeId == longId).FirstOrDefault();
            if (jokeFromDb == null)
                return null;
            var jokeOut = new JokeOut()
            {
                AddedDate = jokeFromDb.AddedDate,
                Content = jokeFromDb.Content,
                Description = jokeFromDb.Description,
                Id = jokeFromDb.Id,
                JokeId = jokeFromDb.JokeId,
                Rate = redisDb.SortedSetScore(keys, jokeFromDb.JokeId.ToString()).ToString()
            };
            return jokeOut;
        }

        public ServiceResponse AddPlus(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new ServiceResponse("Błędna wartość!");
            var jokeId = jokesCollection.Find<Joke>(joke => joke.Id == id).FirstOrDefault().JokeId;
            redisDb.SortedSetIncrement(keys, jokeId.ToString(), 1);
            return new ServiceResponse();
        }

        public ServiceResponse AddMinus(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new ServiceResponse("Błędna wartość!");
            var jokeId = jokesCollection.Find<Joke>(joke => joke.Id == id).FirstOrDefault().JokeId;
            redisDb.SortedSetDecrement(keys, jokeId.ToString(), 1);
            return new ServiceResponse();
        }

        public List<ShortJoke> GetTop10()
        {
            var collection = redisDb.SortedSetRangeByScoreWithScores(keys, Double.NegativeInfinity, Double.PositiveInfinity, Exclude.None,
                Order.Descending, 0, 10);
            var list = new List<ShortJoke>();
            foreach (var sortedSetEntry in collection)
            {
                list.Add(new ShortJoke()
                {
                    Id = sortedSetEntry.Element,
                    Rate = sortedSetEntry.Score.ToString()
                });
            }
            return list;
        }
    }
}
