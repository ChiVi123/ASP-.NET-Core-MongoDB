using DemoUser.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUser.MyDB
{
    public class ConnectDB
    {
        public ConnectDB() { }

        public IMongoDatabase getConnect()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            return client.GetDatabase("MYDB"); ;
        }
    }
}
