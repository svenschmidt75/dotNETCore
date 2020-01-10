using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Mongodb
{
    public class Unit
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("TestDatabase");
            var data = database.GetCollection<Unit>("Units");
            var units = data.Find(new BsonDocument()).ToList();

            Console.WriteLine();
        }
    }
}
