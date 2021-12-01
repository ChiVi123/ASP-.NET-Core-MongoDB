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

<<<<<<< HEAD
        [BsonElement]
        public string title { get; set; }
=======
           [BsonElement]
          public string brief { get; set; }

        [BsonElement]
            public string note { get; set; }
>>>>>>> fdcab8e9979aae6a79a779b0eb99dad591a2309f

        [BsonElement]
        public string note { get; set; }

<<<<<<< HEAD
        [BsonElement]
        public string createdate { get; set; }

        [BsonElement]
        public ObjectId authorid { get; set; }
=======
            [BsonElement]
            public string authorid { get; set; }
    
      
    }
>>>>>>> fdcab8e9979aae6a79a779b0eb99dad591a2309f
    }
}

