using MongoDB.Bson;
using MongoDB.Driver;
using ParserProduct;
using System;
using System.Collections.Generic;

namespace ParserProducts
{
    class MongoDBService : IDBService
    {
        string connectionString; //"mongodb://admin:admin@192.168.0.131:27017";
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public MongoDBService(string mongoAddress, string mongoPort, string mongoUser, string mongoPassword) 
        {
            if (mongoAddress == null || mongoPort == null || mongoUser == null || mongoPassword == null) 
            {
                 throw new Exception("Некоректная строка подключения");
            }

            connectionString = $"mongodb://{mongoUser}:{mongoPassword}@{mongoAddress}:{mongoPort}";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("MainDB");
            BsonDocument ping = database.RunCommand((Command<BsonDocument>)"{ping:1}");

            collection = database.GetCollection<BsonDocument>("Products");
        }

        public async void AddProduct(ProductMeasure product)
        {
            BsonDocument bsonProduct = new BsonDocument
            {
                {"Name", product.Name},
                {"StoreName", product.StoreName},
                {"Price", product.Price},
                {"PriceMeasure", product.PriceMeasure}
            };

            await collection.InsertOneAsync(bsonProduct);
        }

        public ProductMeasure[] GetProduct(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductMeasure> GetProducts()
        {
            var cursor = collection.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursor.ToEnumerable()) 
            {
                var name = document["Name"].AsString;
                var storeName = document["StoreName"].AsString;
                var price = document["Price"].AsDecimal;
                var priceMeasure = document["PriceMeasure"].ToUniversalTime();

                yield return new ProductMeasure(name, price, storeName, priceMeasure);

            }
        }
    }
}
