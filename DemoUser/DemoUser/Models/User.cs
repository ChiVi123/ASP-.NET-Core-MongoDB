using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUser.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /*[BsonElement]
        public int UserID { get; set; }*/

        [BsonElement]
        public string FirstName { get; set; }

        [BsonElement]
        public string LastName { get; set; }

        [BsonElement]
        public string Username { get; set; }

        [BsonElement]
        public string Email { get; set; }

        [BsonElement]
        public string Password { get; set; }
    }
}
