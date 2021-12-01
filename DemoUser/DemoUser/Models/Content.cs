using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUser.Models
{
    public class Content
    {
       
            [BsonId]
            public ObjectId Id { get; set; }

            [BsonElement]
            public string title { get; set; }

           [BsonElement]
          public string brief { get; set; }

        [BsonElement]
            public string note { get; set; }

            [BsonElement]
            public DateTime createdate { get; set; }

            [BsonElement]
            public string authorid { get; set; }
    
      
    }
    }

